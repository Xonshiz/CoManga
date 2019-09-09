# Predefined Variables
$adSdkUrl = "https://admediator.gallerycdn.vsassets.io/extensions/admediator/microsoftadvertisingsdk/10.1.00/1548989510766/MicrosoftAdvertisingSDK.msi"
 $adSdkPath = Join-Path $env:TEMP "MicrosoftAdvertisingSDK.msi"
 
# Download the files to local temp folder
 Write-Output "downloading $adSdkUrl…"
 Invoke-WebRequest -Uri $adSdkUrl -OutFile $adSDKPath
 
# Install the SDKs (use the "qn" flag to install silently)
 Write-Output "installing $adSdkPath…"
 Start-Process $adSdkPath -ArgumentList -Wait