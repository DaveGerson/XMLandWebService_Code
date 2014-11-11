<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:output method="html" />


    <!-- TEMPLATE RULE, MATCHING THE ROOT NODE -->
    <xsl:template match="/">
        <HTML>
            <TABLE BORDER="1" WIDTH="100%">
                <THEAD BGCOLOR="GREY">
                    <TR>
                        <TD>License</TD>
                        <TD>number</TD>
                        <TD>issue_date</TD>
                        <TD>exp_date</TD>
                        <TD>DOB</TD>
                        <TD>End</TD>
                        <TD>Class</TD>
                        <TD>Rest</TD>
                        <TD>Sex</TD>
                        <TD>Height</TD>
                        <TD>Weight</TD>
                        <TD>Eyes</TD>
                        <TD>Hair</TD>
                        <TD>Donor</TD>
                        <TD>First_Name</TD>
                        <TD>Last_Name</TD>
                        <TD>Street</TD>
                        <TD>State</TD>
                        <TD>City</TD>
                        <TD>Zip</TD>
                    </TR>
                </THEAD>
                <TBODY BGCOLOR="WHITE">
                    <xsl:apply-templates select="drivers/license" />
                </TBODY>
            </TABLE>
        </HTML>
    </xsl:template>


    <!-- TEMPLATE RULE, MATCHING <employee> ELEMENTS -->
    <xsl:template match="license">
        <TR>
            <TD>
                <xsl:element name="picture">
                    <img src="{picture}" alt="IMG" style="width:304px;height:228px"/>
                </xsl:element>
            </TD>
            <TD> <xsl:value-of select="number"/>   </TD>
            <TD> <xsl:value-of select="issue_date"/> </TD>
            <TD> <xsl:value-of select="exp_date"/>   </TD>
            <TD> <xsl:value-of select="DOB"/> </TD>
            <TD> <xsl:value-of select="End"/>   </TD>
            <TD> <xsl:value-of select="Class"/> </TD>
            <TD> <xsl:value-of select="Rest"/> </TD>
            <TD> <xsl:value-of select="Sex"/>   </TD>
            <TD> <xsl:value-of select="Height"/> </TD>
            <TD> <xsl:value-of select="Weight"/>   </TD>
            <TD> <xsl:value-of select="Eyes"/> </TD>
            <TD> <xsl:value-of select="Hair"/>   </TD>
            <TD> <xsl:value-of select="Donor"/> </TD>
            <TD> <xsl:value-of select="Name/First"/>   </TD>
            <TD> <xsl:value-of select="Name/Last"/>   </TD>

            <TD> <xsl:value-of select="Address/Street"/> </TD>
            <TD> <xsl:value-of select="Address/State"/> </TD>
            <TD> <xsl:value-of select="Address/City"/> </TD>
            <TD> <xsl:value-of select="Address/Zip"/> </TD>

        </TR>
    </xsl:template>

</xsl:stylesheet>