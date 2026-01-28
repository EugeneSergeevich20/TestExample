<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="xml" indent="yes" encoding="utf-8"/>

	<xsl:template match="/Pay">
		<Employees>
			<xsl:apply-templates select="january/item | february/item | march/item">
				<xsl:sort select="@surname"/>
				<xsl:sort select="@name"/>
			</xsl:apply-templates>
		</Employees>
	</xsl:template>

	<xsl:template match="item">
		<xsl:if test="not(preceding::item[@name=current()/@name and @surname=current()/@surname])">
			<xsl:variable name="currentName" select="@name"/>
			<xsl:variable name="currentSurname" select="@surname"/>

			<Employee name="{$currentName}" surname="{$currentSurname}">
				<xsl:for-each select="/Pay/*/item[@name=$currentName and @surname=$currentSurname]">
					<xsl:sort select="@mount" order="ascending"/>
					<salary amount="{@amount}" mount="{@mount}"/>
				</xsl:for-each>
			</Employee>
		</xsl:if>
	</xsl:template>

</xsl:stylesheet>