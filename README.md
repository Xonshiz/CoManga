# Where to download from?

# Why is it not on application stores?
I have plans on putting these on Play Store (Android), Windows App Store and iOS Application Center. But, currently I do not have that kind of money to invest in all of these together. Hopefully by April 2019 end, I'll push this to Google Play Store and Windows Store as well. iOS is costly AF and will take time...A LOT OF TIME. WHY U DO THIS APPLE?!

# Download Locations?
Since Xamarin Forms is a cross-platform app. developement framework, there are certain things that you need to do platform dependently and getting correct write permissions to save files and creating directories in custom locations is a little tricky. At least it is for me. So, I'm learning along the way and over the time, you'll definitely get a better experience. So, these are some locations you can find your downloaded Comics/Manga. Check the platform-wise locations :

#### Android
You need to browse to your device's "Internal Storage" and there you'll see a folder named "Comic_DL". Inside that folder you'll find your Comics/Manga under their own respective folders.

#### iOS
If you're on iOS, then you're compiling the project and the files will be stored in temp folders of the Simulator. There are multiple steps to this. First, make sure you sort all the folders based on "Last Modified". After that, go to this location :
`/Users/{YourUserName}}/Library/Developer/CoreSimulator/Devices/{SortedFolderOnTop}/data/Containers/Data/Application/{SortedFOlder2OnTop}/Documents/Pictures/`

You should get your downloaded items there.

#### UWP
Getting the permissions is a little tricky in UWP. I've literally tried a lot of different things and couldn't get it to work. So, you'll have to work hard to get these downloaded files. You can find them in this location :
`Put UWP Location Here`

# How To Compile
Install Visual Studio (Recommended Version : 2017). You can install Visual Studio Community version as well, it'll work just fine. Open this project in Visual Studio. Depending upon your device OS, follow these steps :
#### Android
#### iOS
If you have an iPhone, connect it to your device via wires (however iPhones are connected to a system). Or, if you don't have an iPhone, you need macOS to build this. You'll also need to install and updated "XCODE". Then follow this .gif :

#### UWP

# Installing Apps
For installing the application on respective OS, follow the guides below.
#### Android
Download the "CoManga.apk" from the [latest releases] and install as you would install any other apk. You might have to switch on [Allow App Installations From Third Party Vendor](http://www.inbox.com/article/how-do-enable-third-party-apps-on-android.html)
#### iOS
#### UWP

# Special Permissions
#### Android
#### iOS
#### UWP

# Known Issues, Workarounds...
Download button multiple times.

# Why no iOS build? Why do I have to go through such a pain?
I'll break it down in a very very simple manner. I need to have a developer account to be able to push this application to stores. These are the charges for a developer account on these 3 platforms :
* Android : Google Play Store => $25 Lifetime
* Windows Store (UWP) => $19 Lifetime
* iOS App. Store => **$100 PER YEAR** (For Single Developers).

So, I do not make that kind of money and I hope you got the picture why this is not on iOS Stores. Plus, you need an apple device to build the distributable package. I use a Lenovo Laptop for god's sake. But, borrowing a Mac shouldn't be THAT hard, getting the money for the iOS developer account is though.

# Why Xamarin.Forms?
I've been working with Xamarin.Forms for over an year now and I like it. Despite of a lot of weird errors and bugs and some other annoying things, I love it. It just easy to grasp and work with. The community support is decent enough now. If you're a Xamarin Developer, you'd understand. And if you're a Xamarin Developer who start 2-4 years ago, you know the pain.
Anyways, the main point was that Xamarin.Forms is a "Cross-Platform" Mobile Developement Framework. So, write in 1 language (C#) and deliver application on 3 platforms, namely, Android, iOS and UWP (Windows 10), while sharing the single code base. Go on and Google it, that'll give you a better idea.

# Manga and Comic Sources?
The current application takes ["MangaRock"](https://mangarock.com/) as a source for Manga and ["ReadComicsOnline.Me"](https://readcomicsonline.me/) as a source for fetching Comics. These are subjected to change if anything goes down or there some issue. But, for now, we'll go with these two. I initially wanted to go with ["Readcomiconline.To"](https://readcomiconline.to/) as a source for Comics, but it runs behind CloudFlare and it's a hassle to bypass it. Sure, comic-dl cli implements it and thanks to anorov's cf-scrape, it all works. But, it's not a C# based solution. And Believe me, if you've worked with Xamarin, it's a real hassle to deal with a lot of times. So, in short, I didn't want to over complicate things with CloudFlare, so in the end, I went with ReadComicsOnline.Me as a source for comics.

# What if I want to download from somewhere else?
Currently there is no such functionality in this version of the application. However, in the future, I have plans of adding this functionality. It might be limited, but it'll be there. For now, you can directly search your required Comic or Manga from the Search Bar in the respective sections and download the comics. If for some reason, the comic/manga you're looking for is not available on either of the platforms, you can head to comic-dl cli.

# How long till we can download from custom websites?
No Clue. Could be next week, could be next month, or could be next year. Depends on the amount of time I have and the motivation I have to work on this. Btw, you're all welcome to pull this, work on it and send Pull Requests.

# Why Ads?
So, this was a big decision for me to make. If you read the "Why No iOS Build?" section, you'll see that I need money to get an iOS developer account. Donations are always open, but I do need some extra cash and donations aren't much. So, I'll try to save and invest some in it. Don't worry, as the advertisements are just small banners. I promise that no ad will "pop up" or get in your way. I understand it's annoying. So, you will NEVER get those kinds of ads, if you use from this source.
