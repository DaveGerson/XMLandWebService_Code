<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:output method="xml" />


  <!-- TEMPLATE RULE, MATCHING <employee> ELEMENTS -->
  <xsl:template match="/">
    <catalog>
      <xsl:for-each select="catalog/book">
        <book>
          <xsl:attribute name="id">
            <xsl:value-of select="@id" />
          </xsl:attribute>
          <xsl:attribute name="author">
            <xsl:value-of select="author" />
          </xsl:attribute>
          <xsl:attribute name="title">
            <xsl:value-of select="title" />
          </xsl:attribute>
          <xsl:attribute name="genre">
            <xsl:value-of select="genre" />
          </xsl:attribute>
          <xsl:attribute name="price">
            <xsl:value-of select="price" />
          </xsl:attribute>
          <xsl:attribute name="publish_date">
            <xsl:value-of select="publish_date" />
          </xsl:attribute>
          <xsl:attribute name="description">
            <xsl:value-of select="description" />
          </xsl:attribute>
        </book>
      </xsl:for-each>
      </catalog>
  </xsl:template>

</xsl:stylesheet>