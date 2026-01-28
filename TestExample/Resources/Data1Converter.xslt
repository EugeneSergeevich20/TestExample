<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes"/>

	<xsl:key name="employee-name" match="item" use="concat(@name, '|', @surname)"/>

	<xsl:template match="/Pay">
		<Employees>
			<xsl:for-each select="item[generate-id() = generate-id(key('employee-name', concat(@name, '|', @surname))[1])]">
				<xsl:variable name="currentName" select="@name"/>
				<xsl:variable name="currentSurname" select="@surname"/>
				<Employee name="{$currentName}" surname="{$currentSurname}">
					<xsl:for-each select="key('employee-name', concat($currentName, '|', $currentSurname))">
						<salary amount="{@amount}" mount="{@mount}"/>
					</xsl:for-each>
				</Employee>
			</xsl:for-each>
		</Employees>
	</xsl:template>
</xsl:stylesheet>