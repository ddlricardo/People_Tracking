#include "PeopleTrackDll.h"


#define CENTRAL_CROP true   //true:训练时，对96*128的INRIA正样本图片剪裁出中间的64*128大小人体

//继承自CvSVM的类，因为生成setSVMDetector()中用到的检测子参数时，需要用到训练好的SVM的decision_func参数，
//但通过查看CvSVM源码可知decision_func参数是protected类型变量，无法直接访问到，只能继承之后通过函数访问
class MySVM : public CvSVM  
{  
public:  
	//获得SVM的决策函数中的alpha数组
	double * get_alpha_vector()  
	{  
		return this->decision_func->alpha;  
	}  

	//获得SVM的决策函数中的rho参数,即偏移量  
	float get_rho()  
	{  
		return this->decision_func->rho;  
	}
};


/****************************进行训练**************************************/
int trainSVM(char* PosSamPath, char* PosSamList, int PosSamNO, char* NegSamPath, char* NegSamList, int NegSamNO)
{
	//检测窗口(64,128),块尺寸(16,16),块步长(8,8),cell尺寸(8,8),直方图bin个数9
	HOGDescriptor hog(Size(64,128),Size(16,16),Size(8,8),Size(8,8),9);//HOG检测器，用来计算HOG描述子的
	int DescriptorDim;//HOG描述子的维数，由图片大小、检测窗口大小、块大小、细胞单元中直方图bin个数决定
	MySVM svm;

	string ImgName;//图片名(绝对路径)
	ifstream finPos(PosSamList);//正样本图片的文件名列表
	ifstream finNeg(NegSamList);//负样本图片的文件名列表
	if (!finPos || !finNeg) 
	{
		/*cout<<"File open false!"<<endl;*/
		return -1;
	}

	Mat sampleFeatureMat;//所有训练样本的特征向量组成的矩阵，行数等于所有样本的个数，列数等于HOG描述子维数	
	Mat sampleLabelMat;//训练样本的类别向量，行数等于所有样本的个数，列数等于1；1表示有人，-1表示无人

	//依次读取正样本图片，生成HOG描述子
	for(int num=0; num<PosSamNO && getline(finPos,ImgName); num++)
	{
		/*cout<<"处理："<<ImgName<<endl;*/
		ImgName = PosSamPath + ImgName;//加上正样本的路径名
		Mat src = imread(ImgName);//读取图片
		if(CENTRAL_CROP)
			src = src(Rect(16,0,64,128));//将96*160的INRIA正样本图片剪裁为64*128，即剪去上下左右各16个像素

		vector<float> descriptors;//HOG描述子向量
		hog.compute(src,descriptors,Size(8,8));//计算HOG描述子，检测窗口移动步长(8,8)

		/*cout<<"描述子维数："<<descriptors.size()<<endl;*/

		//处理第一个样本时初始化特征向量矩阵和类别矩阵，因为只有知道了特征向量的维数才能初始化特征向量矩阵
		if( 0 == num )
		{
			DescriptorDim = descriptors.size();//HOG描述子的维数
			//初始化所有训练样本的特征向量组成的矩阵，行数等于所有样本的个数，列数等于HOG描述子维数sampleFeatureMat
			sampleFeatureMat = Mat::zeros(PosSamNO+NegSamNO, DescriptorDim, CV_32FC1);
			//初始化训练样本的类别向量，行数等于所有样本的个数，列数等于1；1表示有人，0表示无人
			sampleLabelMat = Mat::zeros(PosSamNO+NegSamNO, 1, CV_32FC1);
		}

		//将计算好的HOG描述子复制到样本特征矩阵sampleFeatureMat
		for(int i=0; i<DescriptorDim; i++)
			sampleFeatureMat.at<float>(num,i) = descriptors[i];//第num个样本的特征向量中的第i个元素
		sampleLabelMat.at<float>(num,0) = 1;//正样本类别为1，有人
	}

	//依次读取负样本图片，生成HOG描述子
	for(int num=0; num<NegSamNO && getline(finNeg,ImgName); num++)
	{
		/*cout<<"处理："<<ImgName<<endl;*/
		ImgName = NegSamPath + ImgName;//加上负样本的路径名
		Mat src = imread(ImgName);//读取图片

		vector<float> descriptors;//HOG描述子向量
		hog.compute(src,descriptors,Size(8,8));//计算HOG描述子，检测窗口移动步长(8,8)

		//将计算好的HOG描述子复制到样本特征矩阵sampleFeatureMat
		for(int i=0; i<DescriptorDim; i++)
			sampleFeatureMat.at<float>(num+PosSamNO,i) = descriptors[i];//第PosSamNO+num个样本的特征向量中的第i个元素
		sampleLabelMat.at<float>(num+PosSamNO,0) = -1;//负样本类别为-1，无人			
	}

	//训练SVM分类器
	//迭代终止条件，当迭代满1000次或误差小于FLT_EPSILON时停止迭代
	CvTermCriteria criteria = cvTermCriteria(CV_TERMCRIT_ITER+CV_TERMCRIT_EPS, 1000, FLT_EPSILON);
	//SVM参数：SVM类型为C_SVC；线性核函数；松弛因子C=0.01
	CvSVMParams param(CvSVM::C_SVC, CvSVM::LINEAR, 0, 1, 0, 0.01, 0, 0, 0, criteria);
	/*cout<<"开始训练SVM分类器"<<endl;*/
	svm.train(sampleFeatureMat,sampleLabelMat, Mat(), Mat(), param);//训练分类器
	/*cout<<"训练完成"<<endl;*/
	svm.save("SVM_HOG.xml");//将训练好的SVM模型保存为xml文件
	return 0;
}


/************************头文件中函数的实现***************************/
int Tracking(bool selfSVM, char* VideoName)
{
	MySVM svm;//SVM分类器
	HOGDescriptor myHOG; //HOG描述子

	//若selfSVM为true，用自己训练的分类器检测
	if (selfSVM)
	{
		svm.load("SVM_HOG.xml");	//从XML文件读取训练好的SVM模型
		//svm.load("SVM_HOG_P2400_N12000.xml");	//从XML文件读取从网上下载的SVM模型

		/*************************************************************************************************
		线性SVM训练完成后得到的XML文件里面，有一个数组，叫做support vector，还有一个数组，叫做alpha,有一个浮点数，叫做rho;
		将alpha矩阵同support vector相乘，注意，alpha*supportVector,将得到一个列向量。之后，再该列向量的最后添加一个元素rho。
		如此，变得到了一个分类器，利用该分类器，直接替换opencv中行人检测默认的那个分类器（cv::HOGDescriptor::setSVMDetector()），
		就可以利用训练样本训练出来的分类器进行行人检测了。
		***************************************************************************************************/
		int DescriptorDim;//HOG描述子的维数，由图片大小、检测窗口大小、块大小、细胞单元中直方图bin个数决定
		DescriptorDim = svm.get_var_count();//特征向量的维数，即HOG描述子的维数
		int supportVectorNum = svm.get_support_vector_count();//支持向量的个数

		Mat supportVectorMat = Mat::zeros(supportVectorNum, DescriptorDim, CV_32FC1);//支持向量矩阵
		Mat resultMat = Mat::zeros(1, DescriptorDim, CV_32FC1);//alpha向量乘以支持向量矩阵的结果

		//将支持向量的数据复制到supportVectorMat矩阵中
		for(int i=0; i<supportVectorNum; i++)
		{
			const float * pSVData = svm.get_support_vector(i);//返回第i个支持向量的数据指针
			for(int j=0; j<DescriptorDim; j++)
				supportVectorMat.at<float>(i,j) = pSVData[j];
		}

		/****************************************************************************************
		opencv2.4.9以前的版本需要使用此段代码

		Mat alphaMat = Mat::zeros(1, supportVectorNum, CV_32FC1);//alpha向量，长度等于支持向量个数
		//将alpha向量的数据复制到alphaMat中
		double * pAlphaData = svm.get_alpha_vector();//返回SVM的决策函数中的alpha向量
		for(int i=0; i<supportVectorNum; i++)
		alphaMat.at<float>(0,i) = pAlphaData[i];
		//计算-(alphaMat * supportVectorMat),结果放到resultMat中
		resultMat = -1 * alphaMat * supportVectorMat;
		*****************************************************************************************/

		//opencv2.4.9之后的版本已经完成了support_vector*alpha的工作，所以alphaMat = [1]，不需要再加权
		resultMat = -1 * supportVectorMat;

		//得到最终的setSVMDetector(const vector<float>& detector)参数中可用的检测子
		vector<float> myDetector;
		//将resultMat中的数据复制到数组myDetector中
		for(int i=0; i<DescriptorDim; i++)
		{
			myDetector.push_back(resultMat.at<float>(0,i));
		}
		//最后添加偏移量rho，得到检测子
		myDetector.push_back(svm.get_rho());
		/*cout<<"检测子维数："<<myDetector.size()<<endl;*/
		//设置HOGDescriptor的检测子
		myHOG.setSVMDetector(myDetector);
	}
	//selfSVM为false，使用opencv自带特征库
	else
	{
		myHOG.setSVMDetector(cv::HOGDescriptor::getDefaultPeopleDetector());	//opencv自带的特征库
	}

	/**************逐帧读取进行HOG行人检测******************/
	cvNamedWindow("行人跟踪");
	/*string ddl_video;
	ddl_video = VideoName;*/
	VideoCapture cap(VideoName);
	if (!cap.isOpened())
	{
		//cout << "不能打开视频文件" << endl;
		return -1;
	}
	Mat frame;
	while(1)
	{
		bool bSuccess = cap.read(frame);
		if (!bSuccess)
		{
			//cout <<"不能从视频文件读取帧，视频已结束"<< endl;
			break;
		}

		//long beginTime = clock();	//获得开始时间，单位为毫秒

		vector<Rect> found, found_filtered;//矩形框数组
		myHOG.detectMultiScale(frame, found, 0, Size(8,8), Size(16,16), 1.1, 2);//对图片进行多尺度行人检测
		//frame为输入待检测的图片；found为检测到目标区域列表；参数3为程序内部计算为行人目标的阈值，也就是检测到的特征到SVM分类超平面的距离;
		//参数4为滑动窗口每次移动的距离。它必须是块移动的整数倍；参数5为图像扩充的大小；参数6为比例系数，即测试图片每次尺寸缩放增加的比例；
		//参数7为组阈值，即校正系数，当一个目标被多个窗口检测出来时，该参数此时就起了调节作用，为0时表示不起调节作用。

		//找出所有没有嵌套的矩形框r,并放入found_filtered中,如果有嵌套的话,则取外面最大的那个矩形框放入found_filtered中
		for(int i=0; i < found.size(); i++)
		{
			Rect r = found[i];
			int j=0;
			for(; j < found.size(); j++)
				if(j != i && (r & found[j]) == r)
					break;
			if( j == found.size())
				found_filtered.push_back(r);
		}

		//画矩形框，因为hog检测出的矩形框比实际人体框要稍微大些,所以这里需要做一些调整
		for(int i=0; i<found_filtered.size(); i++)
		{
			Rect r = found_filtered[i];
			r.x += cvRound(r.width*0.1);
			r.width = cvRound(r.width*0.8);
			r.y += cvRound(r.height*0.1);
			r.height = cvRound(r.height*0.8);
			rectangle(frame, r.tl(), r.br(), Scalar(0,255,0), 3);
		}

		//显示检测到的人数
		for(int x = 0;x < 30;x++)
		{
			for(int y = 0;y < 180;y++)
			{
				for(int c = 0;c < 3;c++)
					frame.at<Vec3b>(x,y)[c] = 255;
			}
		}
		char str_i[127];
		sprintf(str_i,"People Amount:%d",(found_filtered.size()));
		putText(frame, str_i, cvPoint(10,20),CV_FONT_HERSHEY_PLAIN,1,Scalar(0,0,0));

		//long endTime=clock();//获得结束时间
		//cout<<"单次识别花费时间："<<endTime - beginTime<<"ms"<<endl;


		imshow("行人跟踪",frame);

		//按ESC键退出
		char c = cvWaitKey(33);  
		if(c == 27)
			break;
	}
	cap.release();
	cvDestroyWindow("行人跟踪");
	return 0;
}