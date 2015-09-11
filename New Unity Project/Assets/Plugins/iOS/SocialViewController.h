
#import <UIKit/UIKit.h>

@interface SocialViewController : UIViewController
- (int) shareWithFacebook: (NSString *)title message:(NSString *)message image:(NSString *)image;
- (int) shareWithTwitter: (NSString *)title message:(NSString *)message image:(NSString *)image;
@end
