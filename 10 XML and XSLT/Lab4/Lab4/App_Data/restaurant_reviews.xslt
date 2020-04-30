<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">

  <xsl:output method="xml" indent="yes"/>

  <!--creating parameter to receive user input:-->
  <xsl:param name ="minRating"/>
  
  <!--starting up the root:-->
  <xsl:template match="/">
    <html>
      <body>
        <!--displaying messages for the user:-->
        <xsl:choose>
          <!--if counting restaurants with rating on xml greater than rating from user input is equal to 0:-->              
          <xsl:when test="count(/restaurants/restaurant[Rating > $minRating]) = 0">
              No restaurants have a rating above <xsl:value-of select="$minRating" />
          </xsl:when>
         <!--else:-->
          <xsl:otherwise>
            Displaying <xsl:value-of select="count(/restaurants/restaurant[Rating > $minRating])"/> available restaurant(s) above the selected rating:
          </xsl:otherwise>
        </xsl:choose>
        
        
        
        
        
        <xsl:apply-templates select="/restaurants/restaurant" >
          <!--ordering restaurants by deschending rating order-->
          <xsl:sort select="Rating" order="descending"/>
        </xsl:apply-templates>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="/restaurants/restaurant">

    <!--if rating from xml is greater than grading from user input:-->


    <xsl:if test="Rating > $minRating" >

      <!--restaurant name-->
      <H2>
        <xsl:value-of select="name" />
      </H2>

      <!--restaurant address and phone number:-->
      <li>
        Address: <xsl:value-of select="Address/street" />, <xsl:value-of select="Address/city" />, <xsl:value-of select="Address/province" />, <xsl:value-of select="Address/postalCode" />
      </li>
      <li>
        Phone: (<xsl:value-of select="Phone/areaCode" />) <xsl:value-of select="Phone/number" />
      </li>

      <!--summary:-->
      <br/>
      <br/>
      <H3>Summary</H3>
      <ul>
        <xsl:value-of select="Summary" />
      </ul>

      <!--rating:-->
      <h4>
        Rating: <xsl:value-of select="Rating" />
      </h4>

      <!--menu table:-->
      <!--header-->
      <br/>
      <br/>
      <H3>Menu</H3>
      <table border ="1">
        <tr>
          <th>Description</th>
          <th>Quantity</th>
          <th>Price</th>
        </tr>

        <!--appetizers-->
        <tr>
          <th colspan="3">Appetizers</th>
        </tr>

          <xsl:for-each select="menu/Appetizers/Appetizer">
          <xsl:sort select="description" order="ascending"/>
          <tr>
            <td>
              <xsl:value-of select="description" />
            </td>
            <td>
              <xsl:choose>
                <xsl:when test="price[@quantity]">
                  <xsl:value-of select="price/@quantity" />
                </xsl:when>
                <xsl:otherwise>
                 1
                </xsl:otherwise>
              </xsl:choose>
              
              
            </td>
            <td>
              <xsl:value-of select="price" />
            </td>
          </tr>
        </xsl:for-each>

        <!--entrees-->
        <tr>
          <th colspan="3">Entrees</th>
        </tr>
        <xsl:for-each select="menu/Entrees/Entree">
          <xsl:sort select="description" order="ascending"/>
          <tr>
            <td>
              <xsl:value-of select="description" />
            </td>
            <td>
              <xsl:choose>
                <xsl:when test="price[@quantity]">
                  <xsl:value-of select="price/@quantity" />
                </xsl:when>
                <xsl:otherwise>
                  1
                </xsl:otherwise>
              </xsl:choose>
            </td>
            <td>
              <xsl:value-of select="price" />
            </td>
          </tr>
        </xsl:for-each>
      </table>
    </xsl:if>
  </xsl:template>
</xsl:stylesheet>

