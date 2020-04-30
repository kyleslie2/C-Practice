<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="html" indent="yes"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>Itineraries</title>
        <link rel="stylesheet" type="text/css" href="SiteStyles.css"/>
      </head>
      <body>
        <!--a. (2 points) A H1 heading shows the number of passengers found in the itineraries.xml file.-->
        <H1>
          Itineraries of <xsl:value-of select="count(/itineraries/itinerary/passenger)"/> passengers
        </H1>
        <div>

          <xsl:for-each select="/itineraries/itinerary">
            <!--(2 points) Passenger’s name as H2 heading-->
            <H2 colspan="3">
              Passenger: <xsl:value-of select="passenger" />
            </H2>
            <!--(6 points) A table shows each passenger’s itinerary as:-->
                      <!--- *Outbound departure city and arriving city-->
                      <!--- *Inbound departure city and arriving city-->
            <table border="2">
            <tr>
              <th> </th>
              <th>Departure</th>
              <th>Arriving</th>
            </tr>
            <tr>
              <td>
                Outbound
              </td>
              <td>
                <xsl:value-of select="outbound/departure/city" />
              </td>
              <td>
                <xsl:value-of select="outbound/arriving/city" />
              </td>
            </tr>
            <tr>
              <td>
                Inbound
              </td>
              <td>
                <xsl:value-of select="inbound/departure/city" />
              </td>
              <td>
                <xsl:value-of select="inbound/arriving/city" />
              </td>
            </tr>
            </table>
          </xsl:for-each>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
		  




 
  

 
 

