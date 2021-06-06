 -- select da proc sp_helptext spi_ExportaSantanderCartao_v3  

  ----------------------------------------------------------------------
  

    SELECT * FROM trackingcontrole WHERE PedidoId=365271285
	SELECT * FROM Cliente WHERE nome LIKE '%santander%'
	SELECT DtExportacao,SlaProcessado,DtTrackingSla,* FROM Tracking WHERE PedidoId= 365271285  --tabela onde tem as datas de exportação. 

		SELECT * FROM PedidoMestre WHERE Documento='TX297728259BR'



  SELECT numarebct,* FROM pedidomestre  WHERE pedidoid= 365271285 -- numarebct null


---------------------------------------------------------------------------------------- 
-- executar SLA_ProcessarOcorrencias com a data que está pendente o processamento, em seguida executar a  spi_ExportaSantanderCartao_v3

  SLA_ProcessarOcorrencias 1, 137,'2021-03-08 15:29:58.247','2021-03-23 07:30:24.040' 
  spi_ExportaSantanderCartao_v3


SELECT pedidoid, statusid,dtinclusao,dtexportacao,slaprocessado,* FROM Tracking WHERE pedidoid IN (366878880,
-----------------------------------------------------------------------------------------------
--select para mostrar os pedidos que não estão processados da ultima data da proc SLA_ProcessarOcorrencias 

  	SELECT  pm.pedidoid,t.statusid,t.dtinclusao, t.DtExportacao,t.SlaProcessado,T.DtTrackingSla
	FROM Tracking  t (NOLOCK)
	INNER JOIN pedidomestre pm(NOLOCK) ON pm.pedidoid = T.pedidoid AND pm.clienteid=137
	WHERE SlaProcessado=0 AND statusid=1 AND dtexportacao IS NULL AND t.dtinclusao BETWEEN  GETDATE()-30 AND 
	(
		SELECT   
  hist.run_datetime  
 FROM   
  msdb.dbo.sysjobs j  
  CROSS APPLY (  
   SELECT TOP 1  
    CONVERT(DATETIME, RTRIM(run_date))  
    + ((run_time / 10000 * 3600)   
    + ((run_time % 10000) / 100 * 60)   
    + (run_time % 10000) % 100) / (86399.9964) AS run_datetime  
    , *  
   FROM  
       msdb..sysjobhistory sjh  
   WHERE  
       sjh.step_id = 0   
       AND sjh.run_status = 1   
       AND sjh.job_id = j.job_id  
   ORDER BY  
       run_datetime DESC  
  )hist  
 WHERE   
  (name = 'Sla_processar_tracking_entrega')  
  --(name = N'Sla_processar_tracking')  
   
)


