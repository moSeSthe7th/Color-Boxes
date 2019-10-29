//
//  ShortVibration.h
//  VibrationBundle
//
//  Created by Sinan Cem Çaylı on 13.09.2018.
//  Copyright © 2018 Sinan Cem Çaylı. All rights reserved.
//
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

#ifndef ShortVibration_h
#define ShortVibration_h

extern "C" void Vibrate(int x);

typedef enum {
    FeedbackType_Selection,
    FeedbackType_Impact_Light,
    FeedbackType_Impact_Medium,
    FeedbackType_Impact_Heavy,
    FeedbackType_Notification_Success,
    FeedbackType_Notification_Warning,
    FeedbackType_Notification_Error
}FeedbackType;

@interface HapticHelper : NSObject

+ (void)generateFeedback:(FeedbackType)type;

@end


#endif   */ /*ShortVibration_h*/
