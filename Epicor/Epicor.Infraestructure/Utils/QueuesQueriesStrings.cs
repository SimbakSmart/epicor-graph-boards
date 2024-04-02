

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


    }
}
