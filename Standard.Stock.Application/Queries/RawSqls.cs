namespace Standard.Stock.Application.Queries
{
    public static class RawSqls
    {
		public static string Tradings 
		{
			get 
			{
				return $@"
					WITH BUYS AS
					(
						SELECT				 COUNT(A.TransactionId) AS BUY
											,A.Initials
						FROM				[dbo].[Transaction] (NOLOCK) A
						WHERE				A.Type = 1
						AND					CONVERT(DATE, A.[Create]) = CONVERT(DATE, @Create)
						GROUP BY			A.Initials
					),
					SELLS AS
					(
						SELECT				 COUNT(A.TransactionId) AS SELL
											,A.Initials
						FROM				[dbo].[Transaction] (NOLOCK) A
						WHERE				A.Type = 2
						AND					CONVERT(DATE, A.[Create]) = CONVERT(DATE, @Create)
						GROUP BY			A.Initials
					),
					SUMMARY AS
					(
						SELECT DISTINCT		A.*
						FROM				[dbo].[Transaction] (NOLOCK) A
					)
					SELECT DISTINCT		 A.Initials
										,CAST(ROUND(AVG(A.Price), 2) AS decimal(18,2)) AS Average
										,B.BUY AS TotalOfBuys
										,C.SELL AS TotalOfSells
					FROM				SUMMARY A
					LEFT JOIN			BUYS B
					ON					A.Initials = B.Initials
					LEFT JOIN			SELLS C
					ON					A.Initials = C.Initials
					WHERE				(A.Initials = @Initials OR @Initials IS NULL) 
					AND					(CONVERT(DATE, A.[Create]) = CONVERT(DATE, @Create) OR @Create IS NULL)
					GROUP BY			A.Initials, 
										B.BUY,
										C.SELL
				";
			}
		}

		public static string LastTrade
		{
			get 
			{
				return "SELECT CONVERT(DATE, MAX([Create])) FROM [dbo].[Transaction]";
			}
		}
		public static string Transactions
		{
			get 
			{
				return @"
					SELECT		 TOP (100)
								 [TransactionId]
								,[MainTransactionId]
								,[Initials]
								,[Type]
								,[Price]
								,[Quantity]
								,[IsComplete]
								,[Create]
					FROM		[dbo].[Transaction]
					ORDER BY	[Create] 
					DESC
				";
			}
		}
    }
}
