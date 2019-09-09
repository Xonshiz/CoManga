Write-Host "Installing Microsoft Universal Ad Client SDK..."
$msiPath = "$($env:USERPROFILE)\AdMediator.msi"
(New-Object Net.WebClient).DownloadFile('https://visualstudiogallery.msdn.microsoft.com/401703a0-263e-4949-8f0f-738305d6ef4b/file/146057/6/AdMediator.msi', $msiPath)
cmd /c start /wait msiexec /i $msiPath /quiet
Write-Host "Installed" -ForegroundColor green