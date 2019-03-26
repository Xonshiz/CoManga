[![N|Solid](https://github.com/Xonshiz/CoManga/blob/master/OnlineAssets/text_logo.png?raw=true)](https://github.com/Xonshiz/CoManga/)
# CoManga | [![Documentation Status](https://readthedocs.org/projects/CoManga/badge/?version=latest)](http://CoManga.readthedocs.io/en/latest/?badge=latest) | [![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.me/xonshiz)  | [![GitHub release](https://img.shields.io/github/release/xonshiz/CoManga.svg?style=flat-square)](https://github.com/xonshiz/CoManga/releases/latest) | [![Github All Releases](https://img.shields.io/github/downloads/xonshiz/CoManga/total.svg?style=flat-square)](https://github.com/xonshiz/CoManga/releases)

CoManga is a "Cross-Platform Application" that can download various Comics and Manga for free. The application can run on Android, iOS and UWP. It has features like :
- Download any Manga You Want.
- Download any Comic You Want.
- Support Android, iOS and UWP.
- Has parallel download support.
- Shows the latest Manga and Comics.
- Downloads a Single Chapter and puts in a directory with the Comic/Manga name, volume and chapter.
- Downloads all the chapters available for a series.
- Skip if the file has already been downloaded.
- Show human readable error(s) in most places.

## Table of Content

* [Supported Platforms/OS](#supported-platformsos)
* [Where To Download CoManga From?](#where-to-download-comanga-from)
* [Where Does CoManga Place The Files I Download?](#where-does-comanga-place-the-files-i-download)
* [Why Is It Not On Application Stores?](#why-is-it-not-on-application-stores)
* [Why No iOS build? Why do I Have To Go Through Such A Pain?](#why-no-ios-build-why-do-i-have-to-go-through-such-a-pain)
* [Installing The Application](#installing-the-application)
* [Special Permissions](#special-permissions)
* [Opening An Issue/Requesting A Feature](#opening-an-issuerequesting-a-feature)
    * [Reporting Issues](#reporting-issues)
    * [Suggesting A Feature](#suggesting-a-feature)
* [What are the sources for CoManga?](#what-are-the-sources-for-comanga)
* [What if I Want To Download From Somewhere Else?](#what-if-i-want-to-download-from-somewhere-else)
* [How Long Till We Can Download From Custom Websites?](#how-long-till-we-can-download-from-custom-websites)
* [Why Xamarin Forms?](#why-xamarin-forms)
* [Contributing To CoManga](#contribution)
* [How To Compile](#how-to-compile)
* [Know Issues/Workarounds](#know-issuesworkarounds)
* [CoManga Has Advertisements. Why?](#comanga-has-advertisements-why)
* [Changelog](https://github.com/Xonshiz/CoManga/blob/master/Changelog.md)
* [Contributors](https://github.com/Xonshiz/CoManga/blob/master/Contributors.md)
* [Donors](https://github.com/Xonshiz/CoManga/blob/master/Donors.md)
* [Donations](#donations)

# Supported Platforms/OS
CoManga supports :
* Android
* iOS
* UWP

# Where To Download CoManga From?

#### Android
Currently, you can download the Android builds/packages from the [Latest Releases](https://github.com/Xonshiz/CoManga/releases/latest) Section only. 

#### iOS
You'll have to download the source code and compile it yourself. Please refer to the "Compiling" section to know more.

#### UWP (Windows 10)
You can get CoManga directly form the windows store by following this link : [CoManga UWP](https://www.microsoft.com/en-us/p/comanga/9n81f8b5ww93)


I do have future plans to push this to various application stores. For iOS devices, you'll need to build it yourself. Please check that section.

# Where Does CoManga Place The Files I Download?
Since Xamarin Forms is a cross-platform app. developement framework, there are certain things that you need to do platform dependently and getting correct write permissions to save files and creating directories in custom locations is a little tricky. At least it is for me. So, I'm learning along the way and over the time, you'll definitely get a better experience. So, these are some locations you can find your downloaded Comics/Manga. Check the platform-wise locations :

#### Android
You need to browse to your device's `"Internal Storage"` and there you'll see a folder named "CoManga". Inside that folder you'll find your Comics/Manga under their own respective folders.

#### iOS
If you're on iOS, then you're compiling the project and the files will be stored in temp folders of the Simulator. There are multiple steps to this. First, make sure you sort all the folders based on "Last Modified". After that, go to this location :
`/Users/{YourUserName}}/Library/Developer/CoreSimulator/Devices/{SortedFolderOnTop}/data/Containers/Data/Application/{SortedFOlder2OnTop}/Documents/Pictures/`

You should get your downloaded items there.

#### UWP
You can find your downloaded content in the `"Downloads"` folder undeder the folder named "CoManga".


# Why Is It Not On Application Stores?
I have plans on putting these on Play Store (Android), Windows App Store and iOS Application Center. But, currently I do not have that kind of money to invest in all of these together. Hopefully by April 2019 end, I'll push this to Google Play Store and Windows Store as well. iOS is costly AF and will take time...A LOT OF TIME. WHY U DO THIS APPLE?!

# Why No iOS build? Why do I Have To Go Through Such A Pain?
I'll break it down in a very very simple manner. I need to have a developer account to be able to push this application to stores. These are the charges for a developer account on these 3 platforms :
* Android : Google Play Store => $25 Lifetime
* Windows Store (UWP) => $19 Lifetime
* iOS App. Store => **$100 PER YEAR** (For Single Developers).

So, I do not make that kind of money and I hope you got the picture why this is not on iOS Stores. Plus, you need an apple device to build the distributable package. I use a Lenovo Laptop for god's sake. But, borrowing a Mac shouldn't be THAT hard, getting the money for the iOS developer account is though.

# Installing The Application
You need to have certain tools beforehand. So,get :
1.) Visual Studio (Community Version Will Do).
2.) Xamarin

For installing the application on respective OS, follow the guides below.
#### Android
Download the "com.xonshiz.CoManga.apk" from the [Latest Releases](https://github.com/Xonshiz/CoManga/releases/latest) section and install as you would install any other apk. You might have to switch on [Allow App Installations From Third Party Vendor](http://www.inbox.com/article/how-do-enable-third-party-apps-on-android.html).
It's as easy as that.

#### iOS
Currently there's no build for iOS. So, you'll have to compile the project yourself. Check the ["How To Compile"](#how-to-compile) section for details. Note that once you deploy/build the application once on your iOS device, you need not repeat the same steps again and again. It'll install the application on your device, so you can just fire it up and use it.

#### UWP
UWP app is also available in the [Latest Releases](https://github.com/Xonshiz/CoManga/releases/latest) section. You can download the application from there and run it like any other application. You need to download "UWP.CoManga.zip" file and extract it somewhere. Then double click on "comic_dl.UWP_1.0.0.0_x86_x64_arm.appxbundle" and when windows prompts, click on "INSTALL". You're done.


# Special Permissions
Since the application connects to internet and stores files on your system's hard disk, it needs certain permissions from the user. Find the permissions and their usages below :

#### Android
INTERNET : To access the internet.

STORAGE : To store files on the disk.

#### iOS
Calender : Due to apple's policies, we need this. One of the dependency in the application has made this permission necessary. Though, that plugin and this application doesn't use any of this.

#### UWP
Documents Library, Downloads, Pictures : Trying to store the data.


## Opening An Issue/Requesting A Feature
If your're planning to open an issue for the script or ask for a new feature or anything that requires opening an Issue, then please do keep these things in mind.

### Reporting Issues
If you're going to report an issue, then please make sure that the application has all the necessary permissions granted and you're connected to a stable internet connection and you have enough storage space.

Please upload a screenshot of the issue, if possible. Please follow this syntax :

**What You're Trying To Download** : Comic Or Manga

**What Went Wrong?** : What happened?

**Your Device's Operating System** : What OS are you on? Android, iOS or UWP.

**Version Number of Application** : Check from the "Settings" Tab and post it here.
 
### Suggesting A Feature
First things first, please don't make suggestions for these things, as they are already in progress or I have plans of adding them in the application in future.
- Download all the chapters.
- Sorting the chapter list.
- Downloading chapters in a particular range. For Eg : Download 11-16 chapters of a comic/manga.

I don't mean to be rude, but it's such a drag to see the requests for same things over and over again. Please understand. So, if I see any request for either of these things, I'll close the issue immidiately.
However, if you have suggestions on what can be added to these functionalities, please open an issue for the same.

If you're here to make suggestions, please follow the basic syntax to post a request :

**Subject** : Something that briefly tells us about the feature.

**Long Explanation** : Describe in details what you want and how you want.

This should be enough, but it'll be great if you can add more ;)


# What are the sources for CoManga?
The current application takes ["MangaRock"](https://mangarock.com/) as a source for Manga and ["ReadComicsOnline.Me"](https://readcomicsonline.me/) as a source for fetching Comics. These are subjected to change if anything goes down or there some issue. But, for now, we'll go with these two. I initially wanted to go with ["Readcomiconline.To"](https://readcomiconline.to/) as a source for Comics, but it runs behind CloudFlare and it's a hassle to bypass it. Sure, comic-dl cli implements it and thanks to anorov's cf-scrape, it all works. But, it's not a C# based solution. And Believe me, if you've worked with Xamarin, it's a real hassle to deal with a lot of times. So, in short, I didn't want to over complicate things with CloudFlare, so in the end, I went with ReadComicsOnline.Me as a source for comics.

# What if I Want To Download From Somewhere Else?
Currently there is no such functionality in this version of the application. However, in the future, I have plans of adding this functionality. It might be limited, but it'll be there. For now, you can directly search your required Comic or Manga from the Search Bar in the respective sections and download the comics. If for some reason, the comic/manga you're looking for is not available on either of the platforms, you can head to [comic-dl cli](https://github.com/Xonshiz/comic_dl).

# How Long Till We Can Download From Custom Websites?
No Clue. Could be next week, could be next month, or could be next year. Depends on the amount of time I have and the motivation I have to work on this. Btw, you're all welcome to pull this, work on it and send Pull Requests.

# Why Xamarin Forms?
I've been working with Xamarin.Forms for over an year now and I like it. Despite of a lot of weird errors and bugs and some other annoying things, I love it. It just easy to grasp and work with. The community support is decent enough now. If you're a Xamarin Developer, you'd understand. And if you're a Xamarin Developer who start 2-4 years ago, you know the pain.
Anyways, the main point was that Xamarin.Forms is a "Cross-Platform" Mobile Developement Framework. So, write in 1 language (C#) and deliver application on 3 platforms, namely, Android, iOS and UWP (Windows 10), while sharing the single code base. Go on and Google it, that'll give you a better idea.

# Contributing To CoManga
Contributing to the project is fairly simple. I'd recommend going through the project structure first thoroughly. As you can see, CoManga is divided into 2 sections, i.e., Comic and Manga. The respective code will go in the provided directories. Please keep in mind that you follow these points :
- Update the ["Changelog.md"](https://github.com/Xonshiz/CoManga/blob/master/Changelog.md) and ["docs/Changelog.rst"](https://github.com/Xonshiz/CoManga/blob/master/docs/changelog.rst) in the specified format. Even if it is a small typo fix or an issue fixed.
- Every method you create should have your Github username in this format : @Xonshiz (Xonshiz is my Github Handle)
- Please try to comment the logic wherever possible you're applying because a fellow developer might not understand what you did and why you did it.
- Whatever functionality you add, please make sure that it runs on every platform. The UI may not be pixel perfect, that's fine.

# How To Compile
Install Visual Studio (Recommended Version : 2017). You can install Visual Studio Community version as well, it'll work just fine. Clone this repository, open this project in Visual Studio. Depending upon your device OS, follow these steps :
#### Android

![Android Build](https://github.com/Xonshiz/CoManga/blob/master/OnlineAssets/android_deploying.gif?raw=true)

#### iOS
If you have an iPhone, connect it to your device via wires (however iPhones are connected to a system). Or, if you don't have an iPhone, you need macOS to build this. You'll also need to install and updated "XCODE". Then follow this .gif :

![iOS Build](https://github.com/Xonshiz/CoManga/blob/master/OnlineAssets/ios_deploying.gif?raw=true)

#### UWP
To compile, you can run it like this :
![UWP Build](https://github.com/Xonshiz/CoManga/blob/master/OnlineAssets/uwp_deploying.gif?raw=true)

# Know Issues/Workarounds
Sometimes you'll click the download button and then it'll show "Downloaded" with a blank message. You might have to try to download again. If the problem persists, please feel free to open an issue.

# CoManga Has Advertisements. Why?
So, this was a big decision for me to make. If you read the "Why No iOS Build?" section, you'll see that I need money to get an iOS developer account. Donations are always open, but I do need some extra cash and donations aren't much. So, I'll try to save and invest some in it. Don't worry, as the advertisements are just small banners. I promise that no ad will "pop up" or get in your way. I understand it's annoying. So, you will NEVER get those kinds of ads, if you use from this source.

# Donations
You can always send some money over from this :

Paypal : [![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.me/xonshiz)

Patreon Link : https://www.patreon.com/xonshiz

Any amount is appreciated :)
