Function Get-LangAreas
{
	<#
	.SYNOPSIS
		Gets areas from a Umbraco lang XML file.
	.DESCRIPTION
		Gets areas from a Umbraco lang XML file.
	.PARAMETER xml
		The Umbraco lang XML file to get areas from.
	#>

	Param($xml)
		
	$nodes = $xml.SelectNodes("//area/@alias")
		
	if ($nodes.Count -eq 0) { 
		return @() 
	}
		
	return $nodes.'#text'
}
	
Function Get-LangAreaKeys
{
	<#
	.SYNOPSIS
		Gets area keys from a Umbraco lang XML file.
	.DESCRIPTION
		Gets area keys from a Umbraco lang XML file.
	.PARAMETER xml
		The Umbraco lang XML file to get keys from.
	.PARAMETER area
		The area to get keys for.
	#>

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
	
Function Add-LangArea
{
	<#
	.SYNOPSIS
		Adds an area to a Umbraco lang XML file.
	.DESCRIPTION
		Adds an area to a Umbraco lang XML file.
	.PARAMETER xml
		The Umbraco lang XML file to add an area to.
	.PARAMETER area
		The area to add.
	#>

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

Function Add-LangAreaKey
{
	<#
	.SYNOPSIS
		Adds an area key to a Umbraco lang XML file.
	.DESCRIPTION
		Adds an area key to a Umbraco lang XML file.
	.PARAMETER xml
		The Umbraco lang XML file to add an area key to.
	.PARAMETER area
		The area to add the key to.
	.PARAMETER key
		The key to add.
	.PARAMETER value
		The key value to add.
	#>

	Param($xml, $area, $key, $value)
		
	$node = $xml.SelectSingleNode("//area[@alias='$area']//key[@alias='$key']")
		
	if ($node -ne $null) {
		$node.InnerText = $value
		return
	}
		
	$node = (Add-LangArea $xml $area).AppendChild($xml.CreateNode([System.Xml.XmlNodeType]::Element, "key", $null))
		
	$attribute = $node.Attributes.Append($xml.CreateAttribute("alias"))
	$attribute.InnerText = $key
		
	$node.InnerText = $value
}

Function Remove-LangArea
{
	<#
	.SYNOPSIS
		Removes an area from a Umbraco lang XML file.
	.DESCRIPTION
		Removes an area from a Umbraco lang XML file.
	.PARAMETER xml
		The Umbraco lang XML file to remove an area from.
	.PARAMETER area
		The area to remove.
	#>

	Param($xml, $area)

	$node = $xml.SelectSingleNode("//area[@alias='$area']")

	if (-Not ($node -ne $null)) {
		return
	}

	if ($node.HasChildNodes) {
		return
	}

	$node.ParentNode.RemoveChild($node)
}

Function Remove-LangAreaKey
{
	<#
	.SYNOPSIS
		Removes an area key from a Umbraco lang XML file.
	.DESCRIPTION
		Removes an area key from a Umbraco lang XML file.
	.PARAMETER xml
		The Umbraco lang XML file to remove an area key from.
	.PARAMETER area
		The area to remove the key from.
	.PARAMETER key
		The key to remove.
	#>

	Param($xml, $area, $key)

	$node = $xml.SelectSingleNode("//area[@alias='$area']//key[@alias='$key']")

	if (-Not ($node -ne $null)) {
		return
	}

	$node.ParentNode.RemoveChild($node)
}