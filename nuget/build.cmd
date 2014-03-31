pushd %~dp0
pushd ..\src\HighlightedTextBlock.Control\
msbuild HighlightedTextBlock.Control.csproj /p:Configuration=Release
popd
nuget pack HighlightedTextBlock.nuspec