This is a repository for my amino exam that involves creating a slot machine clone.

This README will also serve as my documentation for the parts of the code that I feel were not explained enough in the comments of the code.

Unity Editor Version: 2022.3.4f1
System setup:
- Controllers
    - GameController
        - controls the game data such as coins, bets, payouts, and wins
        - also initializes the reels to contain necessary symbols
        - calculates the results of the spin
    - SpinController
        - controls when a spin occurs
        - reference to spin button and how it should act
        - spins the reels as needed
    - ReelController
        - spin animation for the reels
        - sets the values for the reels
        - returns spin results to game controller
- Data Classes
    - SymbolData
        - contains id, name, payout
            - id is used for identifying the symbol as well as the sprite to be used
                - note that the sprite is 1 + id
                    - i.e. sprite with square 1 is used for id 0, sprite with square 2 is used for id 1, etc.
            - name is included due to exam specs but not used
            - payout is used to calculate payout for each symbol id
    - LineData
        - contains reelInd
            - reelInd is used to identify index of the reels to check for which lines should be used
Data Sources
- Creation of Symbols and Lines are contained in the GameController scripts under the functions CreateSymbolData() and CreateLineData(), respectively

Scalability
- The system was not fully tested for scalability
- The apk provided in the build folder does not lock the phone into landscape mode 
- Portrait viewing of the project is seen to be scalable on the canvas side, not on the gameobjects side

Flexibility
- The system is only available on Android
- APK may be wonky due to problems in building the APK with Unity
- APK build was done in Android Studio after exporting the project from Unity

MVC
- MVC was somewhat applied but not kept in mind
- No proper class structure was applied to the UI and was mixed with the Controller classes instead
- Could be applied given enough time and planning but was not done so

Future Improvements
- Missing features
    - the project was not properly applied according to the exam specs due to improper time management
    - Features missing include:
        - info button
        - displaying the line payouts
        - Firebase data source
        - Proper organization and separation of data manipulation, controller classes, and UI/UX modifications
        - Target stopping point
- Incorrectly applied features
    - Spin animation is seen to be buggy at times
    - UI needs a lot of work
    - Disabling portrait mode on the APK
    - Proper build using Unity instead of scuffed APK building in Android Studio
    - Line Payouts are not the same as seen in the reference app
        - Line payouts simply count in whatever order, even if scattered
        - Line payouts in the reference app start from left to right and stop when order is broken
    - Hard coding is present at some lines of code due to time pressure
- Possible Additions
    - Method of increasing coins without spinning
        - daily acquisition
        - watching ads
    - Option to increase size of slot machine
    - Increase bets by larger amounts
    - Input text field for bets
    - Implementing story events
    - Buying cosmetics to improve look and feel of the project