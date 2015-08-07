param($installPath, $toolsPath, $package, $project)

. "$PSScriptRoot\ujetps-0.0.1\lang-nuget.ps1"

$projectPath = (Get-Item $project.FullName).Directory.FullName
$umbLangPath = "$projectPath\Umbraco\Config\Lang"
$nugLangPath = "$toolsPath\lang"

if (-Not (Test-Path $umbLangPath)) {
	return
}

if (-Not (Test-Path $nugLangPath)) {
	return
}

Remove-Langs $umbLangPath $nugLangPath