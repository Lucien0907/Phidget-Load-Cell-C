#include <stdio.h>
#include <stdlib.h>
#include <phidget22.h>
#include "PhidgetHelperFunctions.h"

/**
* Creates, configures, and opens a VoltageRatioInput channel.
* Displays Voltage Ratio events for 10 seconds
* Closes out VoltageRatioInput channel
*
* @return 0 if the program exits successfully, 1 if it exits with errors.
*/
int main() {
    printf("starting...");
    sleep(3000);
    PhidgetVoltageRatioInputHandle ch0;
    PhidgetVoltageRatioInputHandle ch1;
    PhidgetVoltageRatioInputHandle ch2;
    PhidgetVoltageRatioInputHandle ch3;
    
    PhidgetVoltageRatioInput_create(&ch0);
    PhidgetVoltageRatioInput_create(&ch1);
    PhidgetVoltageRatioInput_create(&ch2);
    PhidgetVoltageRatioInput_create(&ch3);
    
    Phidget_setDeviceSerialNumber((PhidgetHandle)ch0, 494011);
    Phidget_setDeviceSerialNumber((PhidgetHandle)ch1, 494011);
    Phidget_setDeviceSerialNumber((PhidgetHandle)ch2, 494011);
    Phidget_setDeviceSerialNumber((PhidgetHandle)ch3, 494011);
    
    Phidget_setChannel((PhidgetHandle)ch0, 0);
    Phidget_setChannel((PhidgetHandle)ch1, 1);
    Phidget_setChannel((PhidgetHandle)ch2, 2);
    Phidget_setChannel((PhidgetHandle)ch3, 3);
    
    Phidget_openWaitForAttachment((PhidgetHandle)ch0, 5000);
    Phidget_openWaitForAttachment((PhidgetHandle)ch1, 5000);
    Phidget_openWaitForAttachment((PhidgetHandle)ch2, 5000);
    Phidget_openWaitForAttachment((PhidgetHandle)ch3, 5000);

    double voltage_ratio0, voltage_ratio1, voltage_ratio2 ,voltage_ratio3;
    
    //Calibration
    for(int i = 0; i < 10; i++){
        PhidgetVoltageRatioInput_getVoltageRatio(ch0, &voltage_ratio0);
        PhidgetVoltageRatioInput_getVoltageRatio(ch1, &voltage_ratio1);
        PhidgetVoltageRatioInput_getVoltageRatio(ch2, &voltage_ratio2);
        PhidgetVoltageRatioInput_getVoltageRatio(ch3, &voltage_ratio3);
        Sleep(100);
    }
    
    double cal0 = voltage_ratio0, cal1 = voltage_ratio1, cal2 = voltage_ratio2, cal3 = voltage_ratio3;
    double weight0, weight1, weight2, weight3;
    while(1){
        PhidgetVoltageRatioInput_getVoltageRatio(ch0, &voltage_ratio0);
        PhidgetVoltageRatioInput_getVoltageRatio(ch1, &voltage_ratio1);
        PhidgetVoltageRatioInput_getVoltageRatio(ch2, &voltage_ratio2);
        PhidgetVoltageRatioInput_getVoltageRatio(ch3, &voltage_ratio3);

        weight0 = (-voltage_ratio0 + cal0)*54112-0.10;
        if(weight0<0) weight0 = 0;
        weight1 = (-voltage_ratio1 + cal1)*54825-0.12;
        if(weight1<0) weight1 = 0;
        weight2 = (-voltage_ratio2 + cal2)*54681-0.07;
        if(weight2<0) weight2 = 0;
        weight3 = (-voltage_ratio3 + cal3)*56205-0.12;
        if(weight3<0) weight3 = 0;
        
        printf("Channel 0: %.3f kg", weight0);
        for(int i = (weight0/0.1); i>0; i-- ){
            printf("#");
        }
        printf("\n\n");
        
        printf("Channel 1: %.3f kg", weight1);
        for(int i = (weight1/0.1); i>0; i-- ){
            printf("#");
        }
        printf("\n\n");

        printf("Channel 2: %.3f kg", weight2);
        for(int i = (weight2/0.1); i>0; i-- ){
            printf("#");
        }
        printf("\n\n");

        printf("Channel 3: %.3f kg", weight3);
    	for(int i = (weight3/0.1); i>0; i-- ){
            printf("#");
        }
        printf("\n\n\n");

        Sleep(8);
    }

	/*
	 * Perform clean up and exit
	 */

	printf("\nCleaning up...\n");
	ClosePhidgetChannel((PhidgetHandle)ch0);
	ClosePhidgetChannel((PhidgetHandle)ch1);
	ClosePhidgetChannel((PhidgetHandle)ch2);
	ClosePhidgetChannel((PhidgetHandle)ch3);

	PhidgetVoltageRatioInput_delete(&ch0);
	PhidgetVoltageRatioInput_delete(&ch1);
	PhidgetVoltageRatioInput_delete(&ch2);
	PhidgetVoltageRatioInput_delete(&ch3);
	printf("\nExiting...\n");
	printf("Press ENTER to end program.\n");
	getchar();

	return 0;
}
