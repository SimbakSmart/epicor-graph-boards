

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
    }
}
