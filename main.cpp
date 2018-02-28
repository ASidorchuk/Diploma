#include <opencv2/opencv.hpp>
#include <stdio.h>
#include "main.h"
using namespace std;
using namespace cv;

int main()
{


	FlipRotateImg();

	waitKey(0);
}

void FlipRotateImg()
{
	cout << "Begin work" << endl;
	Mat img = imread("C:\\Users\\ASidorchuk\\Desktop\\FractalDiploma\\build\\images\\lena.bmp");
	imshow("orig", img);


	Mat Vert0 = img.clone();
	Mat Hor0 = img.clone();
	Mat Vert90 = img.clone();
	Mat Hor90 = img.clone();
	Mat Vert180 = img.clone();
	Mat Hor180 = img.clone();
	Mat Vert270 = img.clone();
	Mat Hor270 = img.clone();


	rotate(Vert90, Vert90, ROTATE_90_CLOCKWISE);
	rotate(Vert180, Vert180, ROTATE_180);
	rotate(Vert270, Vert270, ROTATE_90_COUNTERCLOCKWISE);

	flip(Vert0, Hor0, 0);
	flip(Vert90, Hor90, 0);
	flip(Vert180, Hor180, 0);
	flip(Vert270, Hor270, 0);


	imshow("vert0", Vert0);
	imshow("hor0", Hor0);
	imshow("Vert90", Vert90);
	imshow("Hor90", Hor90);
	imshow("Vert180", Vert180);
	imshow("Hor180", Hor180);
	imshow("Vert270", Vert270);
	imshow("Hor270", Hor270);


	Mat reversVert0 = Vert0.clone();
	Mat reversHor0 = Hor0.clone();
	Mat reversVert90 = Vert90.clone();
	Mat reversHor90 = Hor90.clone();
	Mat reversVert180 = Vert180.clone();
	Mat reversHor180 = Hor180.clone();
	Mat reversVert270 = Vert270.clone();
	Mat reversHor270 = Hor270.clone();

	flip(reversHor0, reversHor0, 0); //Hor0

	rotate(reversVert90, reversVert90, ROTATE_90_COUNTERCLOCKWISE);
	//transpose(current, current); //Vert90

	flip(reversHor90, reversHor90, 0);
	rotate(reversHor90, reversHor90, ROTATE_90_COUNTERCLOCKWISE);

	rotate(reversVert180, reversVert180, ROTATE_180);

	flip(reversHor180, reversHor180, 0);
	rotate(reversHor180, reversHor180, ROTATE_180);
	//flip(current, current, 1); //Hor180

	rotate(reversVert270, reversVert270, ROTATE_90_CLOCKWISE);
	//transpose(current, current); //Vert270
	//flip(current, current, 0);
	//flip(current, current, 1);

	flip(reversHor270, reversHor270, 0);
	rotate(reversHor270, reversHor270, ROTATE_90_CLOCKWISE);
	//transpose(current, current);//Hor270
	//flip(current, current, 0);
	//flip(current, current, 0);

	imshow("reversevert0", reversVert0);
	imshow("reversehor0", reversHor0);
	imshow("reverseVert90", reversVert90);
	imshow("reverseHor90", reversHor90);
	imshow("reverseVert180", reversVert180);
	imshow("reverseHor180", reversHor180);
	imshow("reverseVert270", reversVert270);
	imshow("reverseHor270", reversHor270);
}
