	param($installPath, $toolsPath, $package, $project)
	
	# Gets all areas from a XML file.
	Function GetAreas
	{
		Param($xml)
		
		$nodes = $xml.SelectNodes("//area/@alias")
		
		if ($nodes.Count -eq 0) { 
			return @() 
		}
		
		return $nodes.'#text' 
	}
	
	# Get all keys from a XML file.
	Function GetKeys
	{
		Param($xml, $area)
		
		$nodes = $xml.SelectNodes("//area[@alias='$area']//key")
		
		if ($nodes.Count -eq 0) {
			return @{}
		}
	
		$keys = @{}
		
		foreach ($node in $nodes) {
			$attribute = $node.Attributes.GetNamedItem("alias")
			$keys.Add($attribute.Value, $node.InnerText)
		}
		
		return $keys
	}
	
	# Inserts an area into a XML file.
	Function InsertArea
	{
		Param($xml, $area)
		
		$node = $xml.SelectSingleNode("//area[@alias='$area']")
		
		if ($node -ne $null) {
			return $node
		}
		
		$node = $xml.DocumentElement.AppendChild($xml.CreateNode([System.Xml.XmlNodeType]::Element, "area", $null))
		
		$attribute = $node.Attributes.Append($xml.CreateAttribute("alias"))
		$attribute.InnerText = $area
		
		return $node
	}
	
	# Inserts a key into a XML file.
	Function InsertKey
	{
		Param($xml, $area, $key, $value)
		
		$node = $xml.SelectSingleNode("//area[@alias='$area']//key[@alias='$key']")
		
		if ($node -ne $null) {
			$node.InnerText = $value
			return
		}
		
		$node = (InsertArea $xml $area).AppendChild($xml.CreateNode([System.Xml.XmlNodeType]::Element, "key", $null))
		
		$attribute = $node.Attributes.Append($xml.CreateAttribute("alias"))
		$attribute.InnerText = $key
		
		$node.InnerText = $value
	}
	
	$projectPath = (Get-Item $project.FullName).Directory.FullName
	$umbLangPath = "$projectPath\Umbraco\Config\Lang"
	$nugLangPath = "$toolsPath\lang"
		
	if (-Not (Test-Path $umbLangPath)) {
		return
	}
	
	if (-Not (Test-Path $nugLangPath)) {
		return
	}
	
	$nugLangFiles = Get-ChildItem "$nugLangPath\*.xml" -Name
	
	foreach ($nugLangFile in $nugLangFiles) {
		if (-Not (Test-Path "$umbLangPath\$nugLangFile")) {
			continue
		}
		
		$umbLangXml = New-Object XML
		$umbLangXml.Load("$umbLangPath\$nugLangFile")
		
		$nugLangXml = New-Object XML
		$nugLangXml.Load("$nugLangPath\$nugLangFile")
		
		foreach ($area in getAreas $nugLangXml) {
			$keys = GetKeys $nugLangXml $area
			
			foreach ($key in $keys.GetEnumerator()) {
				InsertKey $umbLangXml $area $key.Key $key.Value
			}
		}
		
		$umbLangXml.Save("$umbLangPath\$nugLangFile")
	}