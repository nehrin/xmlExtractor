# xmlExtractor

Serko Expense has a new requirement to import data from text received via email.
The data will either be:
• Embedded as ‘islands’ of XML-like content
• Marked up using XML style opening and closing tags

The following text illustrates this:

Hi Yvaine,
Please create an expense claim for the below. Relevant details are marked up as
requested...
<expense><cost_centre>DEV002</cost_centre>
<total>1024.01</total><payment_method>personal card</payment_method>
</expense>
From: Ivan Castle
Sent: Friday, 16 February 2018 10:32 AM
To: Antoine Lloyd <Antoine.Lloyd@example.com>
Subject: test
Hi Antoine,
Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our
<description>development team’s project end celebration dinner</description> on
<date>Tuesday 27 April 2017</date>. We expect to arrive around
7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
Regards,
Ivan

Created a REST API that is parsing the xml to create expense and reservation for the user.

Usage:

POST http://localhost:56458/api/xmlExtractor 

[body]:
"Hi Yvaine,\r\nPlease create an expense claim for the below.  Relevant details are marked up as requested…\r\n<expense><cost_centre>DEV002</cost_centre> <total>1024.01</total><payment_method>personal card</payment_method> </expense>\r\n From: Ivan Castle  Sent: Friday, 16 February 2018 10:32 AM To: Antoine Lloyd <Antoine.Lloyd@example.com> Subject: test \r\nHi Antoine,\r\nPlease create a reservation at the <vendor>Viaduct Steakhouse</vendor> our <description>development team’s project end celebration dinner</description> on <date>Tuesday 27 April 2017</date>.  We expect to arrive around 7.15pm.  Approximately 12 people but I’ll confirm exact numbers closer to the day.\r\nRegards, Ivan"

To create expense/reservation seperately:
POST http://localhost:56458/api/expense

[body]:
{
    "costCentre":"DEV03",
    "total":205.56,
    "paymentMethod":"company card"
}

To create reservation seperately:

POST http://localhost:56458/api/reservation

[body]:
{
	"vendor": "Viaduct Steakhouse",
    "description": "development team’s project end celebration dinner",
    "reservationTime": "2017-04-27T00:00:00"
}

GET http://localhost:56458/api/reservation
GET http://localhost:56458/api/reservation/{id}
GET http://localhost:56458/api/expense
GET http://localhost:56458/api/expense/{id}
