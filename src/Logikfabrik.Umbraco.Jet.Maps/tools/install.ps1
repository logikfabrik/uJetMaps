param($installPath, $toolsPath, $package, $project)

. "$PSScriptRoot\ups\lang-nuget.ps1"

$projectPath = (Get-Item $project.FullName).Directory.FullName
$umbLangPath = "$projectPath\Umbraco\Config\Lang"
$nugLangPath = "$toolsPath\lang"

if (-Not (Test-Path $umbLangPath)) {
	return
}

if (-Not (Test-Path $nugLangPath)) {
	return
}

Add-Langs $umbLangPath $nugLangPath