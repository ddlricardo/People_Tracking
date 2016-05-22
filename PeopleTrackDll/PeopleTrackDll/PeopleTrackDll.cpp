#include "PeopleTrackDll.h"


#define CENTRAL_CROP true   //true:ѵ��ʱ����96*128��INRIA������ͼƬ���ó��м��64*128��С����

//�̳���CvSVM���࣬��Ϊ����setSVMDetector()���õ��ļ���Ӳ���ʱ����Ҫ�õ�ѵ���õ�SVM��decision_func������
//��ͨ���鿴CvSVMԴ���֪decision_func������protected���ͱ������޷�ֱ�ӷ��ʵ���ֻ�ܼ̳�֮��ͨ����������
class MySVM : public CvSVM  
{  
public:  
	//���SVM�ľ��ߺ����е�alpha����
	double * get_alpha_vector()  
	{  
		return this->decision_func->alpha;  
	}  

	//���SVM�ľ��ߺ����е�rho����,��ƫ����  
	float get_rho()  
	{  
		return this->decision_func->rho;  
	}
};


/****************************����ѵ��**************************************/
int trainSVM(char* PosSamPath, char* PosSamList, int PosSamNO, char* NegSamPath, char* NegSamList, int NegSamNO)
{
	//��ⴰ��(64,128),��ߴ�(16,16),�鲽��(8,8),cell�ߴ�(8,8),ֱ��ͼbin����9
	HOGDescriptor hog(Size(64,128),Size(16,16),Size(8,8),Size(8,8),9);//HOG���������������HOG�����ӵ�
	int DescriptorDim;//HOG�����ӵ�ά������ͼƬ��С����ⴰ�ڴ�С�����С��ϸ����Ԫ��ֱ��ͼbin��������
	MySVM svm;

	string ImgName;//ͼƬ��(����·��)
	ifstream finPos(PosSamList);//������ͼƬ���ļ����б�
	ifstream finNeg(NegSamList);//������ͼƬ���ļ����б�
	if (!finPos || !finNeg) 
	{
		/*cout<<"File open false!"<<endl;*/
		return -1;
	}

	Mat sampleFeatureMat;//����ѵ������������������ɵľ��������������������ĸ�������������HOG������ά��	
	Mat sampleLabelMat;//ѵ����������������������������������ĸ�������������1��1��ʾ���ˣ�-1��ʾ����

	//���ζ�ȡ������ͼƬ������HOG������
	for(int num=0; num<PosSamNO && getline(finPos,ImgName); num++)
	{
		/*cout<<"����"<<ImgName<<endl;*/
		ImgName = PosSamPath + ImgName;//������������·����
		Mat src = imread(ImgName);//��ȡͼƬ
		if(CENTRAL_CROP)
			src = src(Rect(16,0,64,128));//��96*160��INRIA������ͼƬ����Ϊ64*128������ȥ�������Ҹ�16������

		vector<float> descriptors;//HOG����������
		hog.compute(src,descriptors,Size(8,8));//����HOG�����ӣ���ⴰ���ƶ�����(8,8)

		/*cout<<"������ά����"<<descriptors.size()<<endl;*/

		//�����һ������ʱ��ʼ�����������������������Ϊֻ��֪��������������ά�����ܳ�ʼ��������������
		if( 0 == num )
		{
			DescriptorDim = descriptors.size();//HOG�����ӵ�ά��
			//��ʼ������ѵ������������������ɵľ��������������������ĸ�������������HOG������ά��sampleFeatureMat
			sampleFeatureMat = Mat::zeros(PosSamNO+NegSamNO, DescriptorDim, CV_32FC1);
			//��ʼ��ѵ����������������������������������ĸ�������������1��1��ʾ���ˣ�0��ʾ����
			sampleLabelMat = Mat::zeros(PosSamNO+NegSamNO, 1, CV_32FC1);
		}

		//������õ�HOG�����Ӹ��Ƶ�������������sampleFeatureMat
		for(int i=0; i<DescriptorDim; i++)
			sampleFeatureMat.at<float>(num,i) = descriptors[i];//��num�����������������еĵ�i��Ԫ��
		sampleLabelMat.at<float>(num,0) = 1;//���������Ϊ1������
	}

	//���ζ�ȡ������ͼƬ������HOG������
	for(int num=0; num<NegSamNO && getline(finNeg,ImgName); num++)
	{
		/*cout<<"����"<<ImgName<<endl;*/
		ImgName = NegSamPath + ImgName;//���ϸ�������·����
		Mat src = imread(ImgName);//��ȡͼƬ

		vector<float> descriptors;//HOG����������
		hog.compute(src,descriptors,Size(8,8));//����HOG�����ӣ���ⴰ���ƶ�����(8,8)

		//������õ�HOG�����Ӹ��Ƶ�������������sampleFeatureMat
		for(int i=0; i<DescriptorDim; i++)
			sampleFeatureMat.at<float>(num+PosSamNO,i) = descriptors[i];//��PosSamNO+num�����������������еĵ�i��Ԫ��
		sampleLabelMat.at<float>(num+PosSamNO,0) = -1;//���������Ϊ-1������			
	}

	//ѵ��SVM������
	//������ֹ��������������1000�λ����С��FLT_EPSILONʱֹͣ����
	CvTermCriteria criteria = cvTermCriteria(CV_TERMCRIT_ITER+CV_TERMCRIT_EPS, 1000, FLT_EPSILON);
	//SVM������SVM����ΪC_SVC�����Ժ˺������ɳ�����C=0.01
	CvSVMParams param(CvSVM::C_SVC, CvSVM::LINEAR, 0, 1, 0, 0.01, 0, 0, 0, criteria);
	/*cout<<"��ʼѵ��SVM������"<<endl;*/
	svm.train(sampleFeatureMat,sampleLabelMat, Mat(), Mat(), param);//ѵ��������
	/*cout<<"ѵ�����"<<endl;*/
	svm.save("SVM_HOG.xml");//��ѵ���õ�SVMģ�ͱ���Ϊxml�ļ�
	return 0;
}


/************************ͷ�ļ��к�����ʵ��***************************/
int Tracking(bool selfSVM, char* VideoName)
{
	MySVM svm;//SVM������
	HOGDescriptor myHOG; //HOG������

	//��selfSVMΪtrue�����Լ�ѵ���ķ��������
	if (selfSVM)
	{
		svm.load("SVM_HOG.xml");	//��XML�ļ���ȡѵ���õ�SVMģ��
		//svm.load("SVM_HOG_P2400_N12000.xml");	//��XML�ļ���ȡ���������ص�SVMģ��

		/*************************************************************************************************
		����SVMѵ����ɺ�õ���XML�ļ����棬��һ�����飬����support vector������һ�����飬����alpha,��һ��������������rho;
		��alpha����ͬsupport vector��ˣ�ע�⣬alpha*supportVector,���õ�һ����������֮���ٸ���������������һ��Ԫ��rho��
		��ˣ���õ���һ�������������ø÷�������ֱ���滻opencv�����˼��Ĭ�ϵ��Ǹ���������cv::HOGDescriptor::setSVMDetector()����
		�Ϳ�������ѵ������ѵ�������ķ������������˼���ˡ�
		***************************************************************************************************/
		int DescriptorDim;//HOG�����ӵ�ά������ͼƬ��С����ⴰ�ڴ�С�����С��ϸ����Ԫ��ֱ��ͼbin��������
		DescriptorDim = svm.get_var_count();//����������ά������HOG�����ӵ�ά��
		int supportVectorNum = svm.get_support_vector_count();//֧�������ĸ���

		Mat supportVectorMat = Mat::zeros(supportVectorNum, DescriptorDim, CV_32FC1);//֧����������
		Mat resultMat = Mat::zeros(1, DescriptorDim, CV_32FC1);//alpha��������֧����������Ľ��

		//��֧�����������ݸ��Ƶ�supportVectorMat������
		for(int i=0; i<supportVectorNum; i++)
		{
			const float * pSVData = svm.get_support_vector(i);//���ص�i��֧������������ָ��
			for(int j=0; j<DescriptorDim; j++)
				supportVectorMat.at<float>(i,j) = pSVData[j];
		}

		/****************************************************************************************
		opencv2.4.9��ǰ�İ汾��Ҫʹ�ô˶δ���

		Mat alphaMat = Mat::zeros(1, supportVectorNum, CV_32FC1);//alpha���������ȵ���֧����������
		//��alpha���������ݸ��Ƶ�alphaMat��
		double * pAlphaData = svm.get_alpha_vector();//����SVM�ľ��ߺ����е�alpha����
		for(int i=0; i<supportVectorNum; i++)
		alphaMat.at<float>(0,i) = pAlphaData[i];
		//����-(alphaMat * supportVectorMat),����ŵ�resultMat��
		resultMat = -1 * alphaMat * supportVectorMat;
		*****************************************************************************************/

		//opencv2.4.9֮��İ汾�Ѿ������support_vector*alpha�Ĺ���������alphaMat = [1]������Ҫ�ټ�Ȩ
		resultMat = -1 * supportVectorMat;

		//�õ����յ�setSVMDetector(const vector<float>& detector)�����п��õļ����
		vector<float> myDetector;
		//��resultMat�е����ݸ��Ƶ�����myDetector��
		for(int i=0; i<DescriptorDim; i++)
		{
			myDetector.push_back(resultMat.at<float>(0,i));
		}
		//������ƫ����rho���õ������
		myDetector.push_back(svm.get_rho());
		/*cout<<"�����ά����"<<myDetector.size()<<endl;*/
		//����HOGDescriptor�ļ����
		myHOG.setSVMDetector(myDetector);
	}
	//selfSVMΪfalse��ʹ��opencv�Դ�������
	else
	{
		myHOG.setSVMDetector(cv::HOGDescriptor::getDefaultPeopleDetector());	//opencv�Դ���������
	}

	/**************��֡��ȡ����HOG���˼��******************/
	cvNamedWindow("���˸���");
	/*string ddl_video;
	ddl_video = VideoName;*/
	VideoCapture cap(VideoName);
	if (!cap.isOpened())
	{
		//cout << "���ܴ���Ƶ�ļ�" << endl;
		return -1;
	}
	Mat frame;
	while(1)
	{
		bool bSuccess = cap.read(frame);
		if (!bSuccess)
		{
			//cout <<"���ܴ���Ƶ�ļ���ȡ֡����Ƶ�ѽ���"<< endl;
			break;
		}

		//long beginTime = clock();	//��ÿ�ʼʱ�䣬��λΪ����

		vector<Rect> found, found_filtered;//���ο�����
		myHOG.detectMultiScale(frame, found, 0, Size(8,8), Size(16,16), 1.1, 2);//��ͼƬ���ж�߶����˼��
		//frameΪ���������ͼƬ��foundΪ��⵽Ŀ�������б�����3Ϊ�����ڲ�����Ϊ����Ŀ�����ֵ��Ҳ���Ǽ�⵽��������SVM���೬ƽ��ľ���;
		//����4Ϊ��������ÿ���ƶ��ľ��롣�������ǿ��ƶ���������������5Ϊͼ������Ĵ�С������6Ϊ����ϵ����������ͼƬÿ�γߴ��������ӵı�����
		//����7Ϊ����ֵ����У��ϵ������һ��Ŀ�걻������ڼ�����ʱ���ò�����ʱ�����˵������ã�Ϊ0ʱ��ʾ����������á�

		//�ҳ�����û��Ƕ�׵ľ��ο�r,������found_filtered��,�����Ƕ�׵Ļ�,��ȡ���������Ǹ����ο����found_filtered��
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

		//�����ο���Ϊhog�����ľ��ο��ʵ�������Ҫ��΢��Щ,����������Ҫ��һЩ����
		for(int i=0; i<found_filtered.size(); i++)
		{
			Rect r = found_filtered[i];
			r.x += cvRound(r.width*0.1);
			r.width = cvRound(r.width*0.8);
			r.y += cvRound(r.height*0.1);
			r.height = cvRound(r.height*0.8);
			rectangle(frame, r.tl(), r.br(), Scalar(0,255,0), 3);
		}

		//��ʾ��⵽������
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

		//long endTime=clock();//��ý���ʱ��
		//cout<<"����ʶ�𻨷�ʱ�䣺"<<endTime - beginTime<<"ms"<<endl;


		imshow("���˸���",frame);

		//��ESC���˳�
		char c = cvWaitKey(33);  
		if(c == 27)
			break;
	}
	cap.release();
	cvDestroyWindow("���˸���");
	return 0;
}