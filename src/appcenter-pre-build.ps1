Write-Host "Installing Microsoft Universal Ad Client SDK..."
$msiPath = "$($env:USERPROFILE)\AdMediator.msi"
(New-Object Net.WebClient).DownloadFile('https://visualstudiogallery.msdn.microsoft.com/401703a0-263e-4949-8f0f-738305d6ef4b/file/146057/6/AdMediator.msi', $msiPath)
cmd /c start /wait msiexec /i $msiPath /quiet
Write-Host "Installed" -ForegroundColor green

# Predefined Variables
$adSdkUrl = "https://admediator.gallerycdn.vsassets.io/extensions/admediator/microsoftadvertisingsdk/10.1.00/1548989510766/MicrosoftAdvertisingSDK.msi"
 $adSdkPath = Join-Path $env:TEMP "MicrosoftAdvertisingSDK.msi"
 
# Download the files to local temp folder
 Write-Output "downloading $adSdkUrl…"
 Invoke-WebRequest -Uri $adSdkUrl -OutFile $adSDKPath
 
# Install the SDKs (use the "qn" flag to install silently)
 Write-Output "installing $adSdkPath…"
 Start-Process $adSdkPath -ArgumentList "/q" -Wait