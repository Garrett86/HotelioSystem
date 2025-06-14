USE [HotelBookingSystem]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 2025/2/24 下午 03:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[roomId] [int] IDENTITY(1,1) NOT NULL,
	[roomType] [varchar](50) NOT NULL,
	[roomArea] [decimal](3, 2) NOT NULL,
	[maxOccupancy] [smallint] NOT NULL,
	[bedType] [varchar](50) NOT NULL,
	[bedSize] [varchar](50) NOT NULL,
	[price] [money] NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[roomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
