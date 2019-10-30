#import "HapticInterface.h"
#import "ShortVibration.h"


extern "C"
{
    void Vibrate(int x)
    {
		FeedbackType type = (FeedbackType)x;
		
        switch (type) {
        case FeedbackType_Selection:
            UInt32 pop = SystemSoundID(1520);
            AudioServicesPlaySystemSound(pop);
            break;
        case FeedbackType_Impact_Light:
            [HapticHelper generateFeedback:FeedbackType_Impact_Light];
            break;
        case FeedbackType_Impact_Medium:
            [HapticHelper generateFeedback:FeedbackType_Impact_Medium];
            break;
        case FeedbackType_Impact_Heavy:
            [HapticHelper generateFeedback:FeedbackType_Impact_Heavy];
            break;
        case FeedbackType_Notification_Success:
            [HapticHelper generateFeedback:FeedbackType_Notification_Success];
            break;
        case FeedbackType_Notification_Warning:
            [HapticHelper generateFeedback:FeedbackType_Notification_Warning];
            break;
        case FeedbackType_Notification_Error:
            [HapticHelper generateFeedback:FeedbackType_Notification_Error];
            break;
        default:
            break;
		}
    }
}






