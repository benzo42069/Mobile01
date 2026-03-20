// PulseStrike | HapticBridge | Phase 9
#import <UIKit/UIKit.h>

extern "C" {
    void _TriggerImpact(int style) {
        UIImpactFeedbackStyle s;
        switch(style) {
            case 0: s = UIImpactFeedbackStyleLight; break;
            case 1: s = UIImpactFeedbackStyleMedium; break;
            case 2: s = UIImpactFeedbackStyleHeavy; break;
            default: s = UIImpactFeedbackStyleMedium; break;
        }

        UIImpactFeedbackGenerator *gen = [[UIImpactFeedbackGenerator alloc] initWithStyle:s];
        [gen prepare];
        [gen impactOccurred];
    }

    void _TriggerNotification(int type) {
        UINotificationFeedbackGenerator *gen = [[UINotificationFeedbackGenerator alloc] init];
        [gen prepare];
        [gen notificationOccurred:(UINotificationFeedbackType)type];
    }
}
