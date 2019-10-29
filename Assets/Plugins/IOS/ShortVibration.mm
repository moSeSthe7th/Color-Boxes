#import <Foundation/Foundation.h>
#import <AudioToolbox/AudioToolbox.h>
#import "ShortVibration.h"
#import <UIKit/UIKit.h>

extern "C"
{
    void Vibrate(int x)
    {
        if(x == 1)
        {
            generateFeedback:(FeedbackType)FeedbackType_Impact_Light;
            //generateImpactFeedback:UIImpactFeedbackStyleLight;
            //UInt32 pop = SystemSoundID(1520);
            //AudioServicesPlaySystemSound(pop);
        }
       /* else if (x == 2)
        {
            generateFeedback:UIImpactFeedbackStyleMedium;
        }
        else if (x == 3)
        {
            generateFeedback:UIImpactFeedbackStyleHeavy;
        }
        else if (x == 4)
        {
            generateFeedback:UINotificationFeedbackTypeSuccess;
        }
        else if (x == 5)
        {
            generateFeedback:UINotificationFeedbackTypeWarning;
        }
        else if (x == 6)
        {
            generateFeedback:UINotificationFeedbackTypeError;
        }*/
    }
}



+(void)generateFeedback:(FeedbackType)type{
    
    if ([[UIDevice currentDevice] systemVersion].floatValue < 10.0){
        return;
    }
    
    switch (type) {
        case FeedbackType_Selection:
            [self generateSelectionFeedback];
            break;
        case FeedbackType_Impact_Light:
            [self generateImpactFeedback:UIImpactFeedbackStyleLight];
            break;
        case FeedbackType_Impact_Medium:
            [self generateImpactFeedback:UIImpactFeedbackStyleMedium];
            break;
        case FeedbackType_Impact_Heavy:
            [self generateImpactFeedback:UIImpactFeedbackStyleHeavy];
            break;
        case FeedbackType_Notification_Success:
            [self generateNotificationFeedback:UINotificationFeedbackTypeSuccess];
            break;
        case FeedbackType_Notification_Warning:
            [self generateNotificationFeedback:UINotificationFeedbackTypeWarning];
            break;
        case FeedbackType_Notification_Error:
            [self generateNotificationFeedback:UINotificationFeedbackTypeError];
            break;
        default:
            break;
    }
}

+(void)generateSelectionFeedback{
    UISelectionFeedbackGenerator *generator = [[UISelectionFeedbackGenerator alloc] init];
    [generator prepare];
    [generator selectionChanged];
    generator = nil;
}

+(void)generateImpactFeedback:(UIImpactFeedbackStyle)style{
    UIImpactFeedbackGenerator *generator = [[UIImpactFeedbackGenerator alloc] initWithStyle:style];
    [generator prepare];
    [generator impactOccurred]; 
    generator = nil;
}

+(void)generateNotificationFeedback:(UINotificationFeedbackType)notificationType{
    UINotificationFeedbackGenerator *generator = [[UINotificationFeedbackGenerator alloc] init];
    [generator prepare];
    [generator notificationOccurred:notificationType];
    generator = nil;
}



