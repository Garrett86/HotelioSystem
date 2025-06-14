USE [HotelBookingSystem]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 2025/2/24 下午 03:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[bookingId] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NOT NULL,
	[roomId] [int] NOT NULL,
	[checkInDate] [date] NOT NULL,
	[checkOutDate] [date] NOT NULL,
	[bookingDate] [date] NOT NULL,
	[totalAmount] [money] NOT NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[bookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Bookings] FOREIGN KEY([userName])
REFERENCES [dbo].[Customers] ([userName])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Customers_Bookings]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_Bookings] FOREIGN KEY([roomId])
REFERENCES [dbo].[Rooms] ([roomId])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Rooms_Bookings]
GO
