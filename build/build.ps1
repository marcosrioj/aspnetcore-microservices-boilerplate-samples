# COMMON PATHS

$buildFolder = (Get-Item -Path "./" -Verbose).FullName
$slnFolder = Join-Path $buildFolder "../"
$outputFolder = Join-Path $buildFolder "outputs"
$identityServerWebHostFolder = Join-Path $slnFolder "IdentityServer/src/IdentityServer.Web.Host"
$microserviceSampleWebHostFolder = Join-Path $slnFolder "MicroserviceSample/src/MicroserviceSample.Web.Host"

## CLEAR ######################################################################

Remove-Item $outputFolder -Force -Recurse -ErrorAction Ignore
New-Item -Path $outputFolder -ItemType Directory

## RESTORE NUGET PACKAGES #####################################################

Set-Location $slnFolder
dotnet restore

## PUBLISH WEB HOST PROJECT ###################################################

Set-Location $identityServerWebHostFolder
dotnet publish --output (Join-Path $outputFolder "IdentityServerHost")

Set-Location $microserviceSampleWebHostFolder
dotnet publish --output (Join-Path $outputFolder "MicroserviceSampleHost")

## CREATE DOCKER IMAGES #######################################################

# Host
Set-Location (Join-Path $outputFolder "IdentityServerHost")

docker rmi abp/host -f
docker build -t abp/host .

Set-Location (Join-Path $outputFolder "MicroserviceSampleHost")

docker rmi abp/host -f
docker build -t abp/host .

## FINALIZE ###################################################################

Set-Location $outputFolder
