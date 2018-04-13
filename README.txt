README: GERM GAME

Author: Jeffrey Spitz

Overview:
    This file details how to download unity, construct builds, and modify language files of the germ game.

******************** DOWNLOADING UNITY ********************

To properly access files and build the project, we need to install unity personal edition.

Procedure:
1. Go to https://unity3d.com
2. Click on Get Unity or Get Unity Now
3. Find Unity Personal Edition and Click Download Now
4. Download Installer
5. Open the installer and follow the instructions
6. When choosing which unity components you want to download and install make sure to select build options you wish to use in the future. It is recommended you download all Build Supports so you have as many options as possible.


******************** OPENING GERM GAME PROJECT ***********

Procedure:
1. Open Unity App
2. You may have to create an account with unity and fill in some personal information to use the software.
3. By default the App should open up in the projects tab. On the top right hand corner of the window select open and select the file titled GermGame3D.


******************* USING UNITY **************************

PLEASE READ:
You can navigate through different features of the project through unity windows. The names of the windows are indicated by small tabs directly above the windows. If a window is not being displayed, you can find it under the windows tab in the top bar. Most of the files that need to be modified are under the project window. Under the project window make sure you are inside the Assets Folder. By default you should already be inside the Assets Folder and see folders including _Scenes, Dice, Sounds, Scripts, etc. Otherwise you should see the Assets Folder file and should open it to reveal the files.

* Make sure when you are inside the PlayArea scene when trying to modify the game directly. To get to the PlayArea scene: Project Window > _Scenes > PlayArea (Click it to open the scene).

TO TEST THE GAME:
At the top of the application window, click the play button to start the game and click it again to stop.

****************** LANGUAGE FILES ***********************

When modifying language files, you can choose to modify them in unity through the built in MonoDevelop application, or by using a text editor of your choice.

———TO MODIFY LANGUAGE FILES IN UNITY———
In Unity open: Project Window > Scripts > GermGameLanguages


———TO MODIFY LANGUAGE FILES DIRECTLY———
Find the GermGame3D file and open: GermGame3D > Assets > Scripts > GermGameLanguages.xml

Notes:
1. The XML file structure:

<languages>
    <language name=“English”>
        <string name=“ … ”> … </string>
        . . .
        . . .
    </language>

    <language name=“Français”>
        <string name=“ … ”> … </string>
        . . .
        . . .
    </language>
</languages>

2. Text for each language is surrounded by XML <language> tags. For Example the English language is contained within <language name=“English”> … </language> tags.
3. The game accesses text from each language through their string tags (<string name=“…”> … </string>) The names of these tags are the same for all languages and SHOULD NEVER BE MODIFIED!!! Words between the <string name=“…”>and </string> tags are displayed by the game.
4. Modifying the language file WILL NOT change the languages displayed in games you have already built. You must build a new game if you want the changes to be displayed.
5. The built in French translation was constructed using google translate. You may want to modify it before publishing the game.

To Modify an Existing Language:
1. Find the words between <string name=“…”> and </string> tags you want to modify.
2. Refer to the words in between the English <string name=“…”> and </string> tags for reference.
3. Change the words between the <string name=“…”> and </string> tags (this is what is displayed in game).

To Create a New Language:
1. Copy all code between and including <language name=“English”> … </language> tags.
2. Paste the code underneath the last </language> tag in the file
NOTE: Be careful to not paste code under the </languages> tag.
3. Rename <language name=“English”> to <language name=“TARGET LANGUAGE”> (IF YOU DON’T THE GAME WILL ENCOUNTER ERRORS)
4. For all text between <string name=“…”> and </string> tags. Replace the English words with their respective translations.


******************** BUILDING UNITY PROJECT INTO APPS / WEBFILES **********************

Procedure:
1. Go to: File > Build Settings…
2. Under Scenes in Build, make sure the only scene present is _Scenes/PlayArea. If there are other scenes select them and hit delete. If there are no scenes make sure the game is open in the PlayArea scene (Explained under USING UNITY!!!) and click Add Open Scenes.
3. Under Platform, select what platform you want to build for.
4. Click build and select where on your computer you would like to save the build files. (I recommend creating a build folder in documents or on your desktop to save your files so you can easily access them)
5. When the Build is complete, click open the file if it is an application or open the file and click open index.html for web based games.
