Function Add-Lang
{
	<#
	.SYNOPSIS
	.DESCRIPTION
	.PARAMETER umbLangPath
	.PARAMETER nugLangPath
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

Function Remove-Lang
{
	<#
	.SYNOPSIS
	.DESCRIPTION
	.PARAMETER umbLangPath
	.PARAMETER nugLangPath
	#>

	Param($umbLangPath, $nugLangPath)
}