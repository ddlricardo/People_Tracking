#pragma once
#include <fstream>
#include <iostream>
#include <opencv2/ml/ml.hpp>
#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/objdetect/objdetect.hpp>

using namespace std;
using namespace cv;

extern "C" _declspec(dllexport) int Tracking(bool selfSVM, char* VideoName);
extern "C" _declspec(dllexport) int trainSVM(char* PosSamPath, char* PosSamList, int PosSamNO, char* NegSamPath, char* NegSamList, int NegSamNO);