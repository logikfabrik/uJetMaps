. "$PSScriptRoot\lang-xml.ps1"

Function Add-Langs
{
	<#
	.SYNOPSIS
		Adds Umbraco lang keys.
	.DESCRIPTION
		Adds Umbraco lang keys.
	.PARAMETER umbLangPath
		The Umbraco lang directory path.
	.PARAMETER nugLangPath
		The NuGet lang directory path.
	#>

	Param($umbLangPath, $nugLangPath)

	$nugLangFiles = Get-ChildItem "$nugLangPath\*.xml" -Name
	
	foreach ($nugLangFile in $nugLangFiles) {
		if (-Not (Test-Path "$umbLangPath\$nugLangFile")) {
			continue
		}
		
		$umbLangXml = New-Object XML
		$umbLangXml.Load("$umbLangPath\$nugLangFile")
		
		$nugLangXml = New-Object XML
		$nugLangXml.Load("$nugLangPath\$nugLangFile")
		
		foreach ($area in Get-LangAreas $nugLangXml) {
			$keys = Get-LangAreaKeys $nugLangXml $area
			
			foreach ($key in $keys.GetEnumerator()) {
				Add-LangAreaKey $umbLangXml $area $key.Key $key.Value
			}
		}
		
		$umbLangXml.Save("$umbLangPath\$nugLangFile")
	}
}

Function Remove-Langs
{
	<#
	.SYNOPSIS
		Removes Umbraco lang keys.
	.DESCRIPTION
		Removes Umbraco lang keys.
	.PARAMETER umbLangPath
		The Umbraco lang directory path.
	.PARAMETER nugLangPath
		The NuGet lang directory path.
	#>

	Param($umbLangPath, $nugLangPath)

	$nugLangFiles = Get-ChildItem "$nugLangPath\*.xml" -Name
	
	foreach ($nugLangFile in $nugLangFiles) {
		if (-Not (Test-Path "$umbLangPath\$nugLangFile")) {
			continue
		}
		
		$umbLangXml = New-Object XML
		$umbLangXml.Load("$umbLangPath\$nugLangFile")
		
		$nugLangXml = New-Object XML
		$nugLangXml.Load("$nugLangPath\$nugLangFile")
		
		foreach ($area in Get-LangAreas $nugLangXml) {
			$keys = Get-LangAreaKeys $nugLangXml $area
			
			foreach ($key in $keys.GetEnumerator()) {
				Remove-LangAreaKey $umbLangXml $area $key.Key
			}

			Remove-LangArea $umbLangXml $area
		}
		
		$umbLangXml.Save("$umbLangPath\$nugLangFile")
	}
}