

namespace Epicor.Infraestructure.Utils
{
    public static  class QueuesQueriesStrings
    {
        public static string GRAN_TOTAL_TOTAL_CLOSE_AND_TOTAL_OPEN = @"
        SELECT 
        (SELECT COUNT(*) 
	    FROM SupportCall  WHERE YEAR(OpenDate) >=2020) AS Total ,
        (SELECT COUNT(*) 
        FROM SupportCall 
        WHERE YEAR(OpenDate) >= 2020 AND Closed = 1) AS TotalClosed ,
       (SELECT COUNT(*) 
        FROM SupportCall 
        WHERE YEAR(OpenDate) >= 2020 AND Closed = 0) AS TotalOpen;
       ";

        public static string GRAN_TOTAL_TOTAL_CLOSE_AND_TOTAL_OPEN_WITH_FILTERS = @" 
        SELECT 
        (SELECT COUNT(*) 
            FROM SupportCall  
            WHERE YEAR(OpenDate) >= 2020 AND OpenDate >= ? AND OpenDate <= ?) AS Total,    
        (SELECT COUNT(*) 
            FROM SupportCall 
            WHERE YEAR(OpenDate) >= 2020 AND Closed = 1 AND OpenDate >= ? AND OpenDate <= ?) AS TotalClosed,    
        (SELECT COUNT(*) 
            FROM SupportCall 
            WHERE YEAR(OpenDate) >= 2020 AND Closed = 0 AND OpenDate >= ? AND OpenDate <= ?) AS TotalOpen;

         ";

        public static string TOTAL_OPEN_BY_QUEUES_RESPONSABLES = @"
        SELECT
        CASE
        WHEN Q.Name IS NULL THEN 'Asignadas a sistemas'
        ELSE
        REPLACE(Q.Name,'_',' ')
        END AS [Queue],
        COUNT(*) AS Total
        FROM SupportCall AS SC
        LEFT JOIN Queue AS Q  ON Q.QueueID = SC.AssignToQueueID
        WHERE Closed =0 AND YEAR(OpenDate) >= 2020
        GROUP BY  Q.Name ORDER BY TOTAL DESC
        ";

        public static string TOTAL_OPEN_BY_QUEUES_RESPONSABLES_WITH_FILTERS = @"
        SELECT
        CASE
        WHEN Q.Name IS NULL THEN 'Asignadas a sistemas'
        ELSE
        REPLACE(Q.Name,'_',' ')
        END AS [Queue],
        COUNT(*) AS Total
        FROM SupportCall AS SC
        LEFT JOIN Queue AS Q  ON Q.QueueID = SC.AssignToQueueID
        WHERE Closed =0 AND YEAR(OpenDate) >= 2020 AND OpenDate >= ? AND OpenDate <= ?
        GROUP BY  Q.Name ORDER BY TOTAL DESC
        ";

        public static string TOTAL_OPEN_BY_RANGE_DAYS = @"
        SELECT
          ISNULL(Q.Name,'ASIGNADA A USUARIOS') AS [Queue],
          SUM(CASE WHEN DATEDIFF(DAY, OpenDate, GETDATE()) BETWEEN 0 AND 2 THEN 1 ELSE 0 END) AS [RangeOne],
          SUM(CASE WHEN DATEDIFF(DAY, OpenDate, GETDATE()) BETWEEN 3 AND 7 THEN 1 ELSE 0 END) AS [RangeTwo],
          SUM(CASE WHEN DATEDIFF(DAY, OpenDate, GETDATE()) BETWEEN 8 AND 15 THEN 1 ELSE 0 END) AS [RangeThree],
          SUM(CASE WHEN DATEDIFF(DAY, OpenDate, GETDATE()) BETWEEN 16 AND 20 THEN 1 ELSE 0 END) AS [RangeFour],
          SUM(CASE WHEN DATEDIFF(DAY, OpenDate, GETDATE()) >= 21 THEN 1 ELSE 0 END) AS [RangeFive],
          COUNT(*) AS [Total]  
        FROM SupportCall AS Sc
        LEFT JOIN Queue AS  Q ON Q.QueueID = Sc.AssignToQueueID
        WHERE YEAR(Sc.OpenDate) >= 2020 AND SC.Closed=0
        GROUP BY
          Q.Name
        ORDER BY Q.Name;
         ";

        public static string TOTAL_OPEN_BY_RANGE_DAYS_WITH_FILTERS = @"
         SELECT
           ISNULL(Q.Name,'ASIGNADA A USUARIOS') AS [Queue],
           SUM(CASE WHEN DATEDIFF(DAY, OpenDate, GETDATE()) BETWEEN 0 AND 2 THEN 1 ELSE 0 END) AS [RangeOne],
           SUM(CASE WHEN DATEDIFF(DAY, OpenDate, GETDATE()) BETWEEN 3 AND 7 THEN 1 ELSE 0 END) AS [RangeTwo],
           SUM(CASE WHEN DATEDIFF(DAY, OpenDate, GETDATE()) BETWEEN 8 AND 15 THEN 1 ELSE 0 END) AS [RangeThree],
           SUM(CASE WHEN DATEDIFF(DAY, OpenDate, GETDATE()) BETWEEN 16 AND 20 THEN 1 ELSE 0 END) AS [RangeFour],
           SUM(CASE WHEN DATEDIFF(DAY, OpenDate, GETDATE()) >= 21 THEN 1 ELSE 0 END) AS [RangeFive],
           COUNT(*) AS [Total]  
         FROM SupportCall AS Sc
         LEFT JOIN Queue AS  Q ON Q.QueueID = Sc.AssignToQueueID
         WHERE YEAR(Sc.OpenDate) >= 2020 AND SC.Closed=0 
         AND  Sc.OpenDate >= ? AND Sc.OpenDate <= ?
         GROUP BY
           Q.Name
         ORDER BY Q.Name;

        ";


        public static string TOTAL_OPEN_BY_STATUS = @"
        SELECT 
        Scs.Name AS Status,
        COUNT(*) AS Total
        FROM SupportCall AS Sc
        LEFT JOIN SupportCallStatus AS Scs ON Scs.SupportCallStatusID=Sc.StatusID
        WHERE YEAR(Sc.OpenDate) >= 2020 AND Sc.Closed=0 AND Sc.AssignToQueueID IS NOT NULL
        GROUP BY Scs.Name
        ";

        public static string TOTAL_OPEN_BY_STATUS_WITH_FILTERS = @"
        SELECT 
        Scs.Name AS Status,
        COUNT(*) AS Total
        FROM SupportCall AS Sc
        LEFT JOIN SupportCallStatus AS Scs ON Scs.SupportCallStatusID=Sc.StatusID
        WHERE YEAR(Sc.OpenDate) >= 2020 AND Sc.Closed=0 AND Sc.AssignToQueueID IS NOT NULL
        AND OpenDate >= ? AND OpenDate <= ?
        GROUP BY Scs.Name
        ";

        public static string TOTAL_OPEN_BY_URGENCY = @"            
            SELECT 
            CASE
            WHEN U.Entry IS NULL THEN 'NO DEFINITOS'
            ELSE
             SUBSTRING(U.Entry, 4, LEN(U.Entry)) 
            END
            AS [Urgency],
            COUNT(*) AS [Total]
            FROM SupportCall AS Sc
            LEFT JOIN [EpicorITSMApplication].[dbo].[ValueListEntry] AS U   ON U.ValueListEntryID = Sc.UrgencyID
            WHERE YEAR(Sc.OpenDate) >=2020 AND Sc.Closed=0
            GROUP BY U.Entry  ORDER BY Urgency DESC
        ";


    }
}
