SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spwfPreOrderPMDLInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spwfPreOrderPMDLInsert]
GO
 
CREATE PROCEDURE [dbo].[spwfPreOrderPMDLInsert]
  @WFID Int,
  @PMDLDocNo VarChar(100),
  @Return_WFID Int = null OUTPUT, 
  @Return_PMDLDocNo VarChar(100) = null OUTPUT 
  AS
  INSERT [WF1_PreOrderPMDL]
  (
   [WFID]
  ,[PMDLDocNo]
  )
  VALUES
  (
   @WFID
  ,UPPER(@PMDLDocNo)
  )
  SET @Return_WFID = @WFID
  SET @Return_PMDLDocNo = @PMDLDocNo
  GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spwfPreOrderInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spwfPreOrderInsert]
GO
 
CREATE PROCEDURE [dbo].[spwfPreOrderInsert]
  @WFID Int,
  @Parent_WFID Int,
  @Project VarChar(50),
  @Element VarChar(50),
  @SpecificationNo VarChar(100),
  @Buyer VarChar(8),
  @WF_Status VarChar(100),
  @UserId VarChar(8),
  @DateTime DateTime,
  @Supplier NVarChar(150),
  @SupplierName VarChar(100),
  @RandomNo VarChar(8),
  @PMDLDocNo VarChar(200),
  @SupplierCode VarChar(50),
  @ReceiptNo VarChar(9),
  @Manager VarChar(8),
  @EmailSubject VarChar(200),
  @IndentNo VarChar(9),
  @IndentLine Int,
  @LotItem VarChar(47),
  @Return_WFID Int = null OUTPUT 
  AS
  INSERT [WF1_PreOrder]
  (
   [WFID]
  ,[Parent_WFID]
  ,[Project]
  ,[Element]
  ,[SpecificationNo]
  ,[Buyer]
  ,[WF_Status]
  ,[UserId]
  ,[DateTime]
  ,[Supplier]
  ,[SupplierName]
  ,[RandomNo]
  ,[PMDLDocNo]
  ,[SupplierCode]
  ,[ReceiptNo]
  ,[Manager]
  ,[EmailSubject]
  ,[IndentNo]
  ,[IndentLine]
  ,[LotItem]
  )
  VALUES
  (
   @WFID
  ,@Parent_WFID
  ,@Project
  ,@Element
  ,@SpecificationNo
  ,@Buyer
  ,@WF_Status
  ,@UserId
  ,@DateTime
  ,@Supplier
  ,@SupplierName
  ,@RandomNo
  ,@PMDLDocNo
  ,@SupplierCode
  ,@ReceiptNo
  ,@Manager
  ,@EmailSubject
  ,@IndentNo
  ,@IndentLine
  ,@LotItem
  )
  SET @Return_WFID = @WFID
  GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spwfPreOrderHistoryInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spwfPreOrderHistoryInsert]
GO
 
CREATE PROCEDURE [dbo].[spwfPreOrderHistoryInsert]
  @WF_HistoryID Int,
  @WFID Int,
  @WFID_SlNo Int,
  @Parent_WFID Int,
  @Project VarChar(50),
  @Element VarChar(50),
  @SpecificationNo VarChar(100),
  @Buyer VarChar(8),
  @WF_Status VarChar(100),
  @UserId VarChar(8),
  @DateTime DateTime,
  @Supplier VarChar(50),
  @SupplierName VarChar(100),
  @Notes VarChar(500),
  @PMDLDocNo VarChar(100),
  @Manager VarChar(8),
  @IndentNo VarChar(9),
  @IndentLine Int,
  @LotItem VarChar(47),
  @Return_WF_HistoryID Int = null OUTPUT 
  AS
  INSERT [WF1_PreOrder_History]
  (
   [WF_HistoryID]
  ,[WFID]
  ,[WFID_SlNo]
  ,[Parent_WFID]
  ,[Project]
  ,[Element]
  ,[SpecificationNo]
  ,[Buyer]
  ,[WF_Status]
  ,[UserId]
  ,[DateTime]
  ,[Supplier]
  ,[SupplierName]
  ,[Notes]
  ,[PMDLDocNo]
  ,[Manager]
  ,[IndentNo]
  ,[IndentLine]
  ,[LotItem]
  )
  VALUES
  (
   @WF_HistoryID
  ,@WFID
  ,@WFID_SlNo
  ,@Parent_WFID
  ,@Project
  ,@Element
  ,@SpecificationNo
  ,@Buyer
  ,@WF_Status
  ,@UserId
  ,@DateTime
  ,@Supplier
  ,@SupplierName
  ,@Notes
  ,@PMDLDocNo
  ,@Manager
  ,@IndentNo
  ,@IndentLine
  ,@LotItem
  )
  SET @Return_WF_HistoryID = @WF_HistoryID
  GO

SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
