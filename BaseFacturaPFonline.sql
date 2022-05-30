alter procedure sp_busqueda_productos(
@busqueda varchar(50)
)
as
begin
	select facIdFactura,concat(facNumeroDocumento,' - ',facRazonSocial,' - ',cliNombre,' - $/.',facTotal)[Texto],facNumeroDocumento,facFechaIngreso,facRazonSocial,dfoNombre,cliNombre,cliCorreo,cliDireccion,facTotal from Clientes,Factura,Fotografos 
	where concat(facNumeroDocumento,' - ',facRazonSocial,' - $/.',facTotal) like '%'+ @busqueda + '%'
end

	select facIdFactura,concat(facIdFactura,' - ',dfaProducto,' - $/.',dfaTotal) from DetalleFactura

alter procedure sp_busqueda_productosdf(
@busquedadf varchar(50)
)
as
begin
	select dfaIdDetalleFactura,concat(facNumeroDocumento,' - ',dfaProducto,' - $/.',dfaTotal)[Texto],dfaProducto,dfaCantidad,dfaTotal from DetalleFactura,Factura
	where concat(facNumeroDocumento,' - ',dfaProducto,' - $/.',dfaTotal) like '%'+ @busquedadf + '%'
end


exec sp_busqueda_productos '1'

select * from Clientes
select * from DetalleFactura
select * from Factura
select * from Fotografos
CREATE procedure sp_busqueda_productos(
@busqueda varchar(20)
)
as
begin


	select facIdFactura,concat(facNumeroDocumento,' - ',facRazonSocial,' - $/.',facTotal)[Texto],facTotal,facRazonSocial from Factura 
	where concat(facNumeroDocumento,' - ',facRazonSocial,' - $/.',facTotal) like '%'+ @busqueda + '%'
end


create PROC sp_lista_Clientes
as
begin

select cliIdCliente,cliNombre,cliCorreo,cliDireccion,convert(char(10),cliFechaIngreso,103)[cliFechaIngreso] from Clientes

end

exec sp_lista_Clientes

create PROC sp_lista_DetalleFactura
as
begin

select dfaIdDetalleFactura,dfaProducto,dfaPrecio,dfaTotal,convert(char(10),dfaFechaIngreso,103)[dfaFechaIngreso] from DetalleFactura

end

exec sp_lista_DetalleFactura

create PROC sp_lista_Factura
as
begin

select facIdFactura,facRazonSocial,facTotal,convert(char(10),facFechaIngreso,103)[facFechaIngreso] from Factura

end

exec sp_lista_Factura

create PROC sp_lista_Fotografos
as
begin

select dfoIdFotografo,dfoNombre,convert(char(10),dfoFechaIngreso,103)[dfoFechaIngreso] from Fotografos

end

exec sp_lista_Fotografos
alter procedure sp_GuardarFactura(
@Factura_xml xml
)
as
begin
	

	insert into Factura(facNumeroDocumento,facRazonSocial,facTotal,facFechaIngreso)
	select
		nodo.elemento.value('facNumeroDocumento[1]','varchar(20)') [facNumeroDocumento],
		nodo.elemento.value('facRazonSocial[1]','varchar(50)') [facRazonSocial],
		nodo.elemento.value('facTotal[1]','decimal(10,2)') [facTotal],
		nodo.elemento.value('facFechaIngreso[1]','varchar(10)') [facFechaIngreso]
	from @Factura_xml.nodes('Factura') nodo(elemento)


	declare @facIdFactura_generado int = (select max(facIdFactura) from Factura)
	declare @cliIdCliente_generado int = (select max(cliIdCliente) from Clientes)
	declare @dfoIdFotografo_generado int = (select max(dfoIdFotografo) from Fotografos)

	insert into DetalleFactura(cliIdCliente,dfoIdFotografo,facIdFactura,dfaProducto,dfaPrecio,dfaCantidad,dfaTotal,dfaFechaIngreso)
	select
		@cliIdCliente_generado [idventa],
		@dfoIdFotografo_generado [idventa],
		@facIdFactura_generado [facIdFactura],
		nodo.elemento.value('dfaProducto[1]','varchar(50)') [dfaProducto],
		nodo.elemento.value('dfaPrecio[1]','decimal(10,2)') [dfaPrecio],
		nodo.elemento.value('dfaCantidad[1]','int') [dfaCantidad],
		nodo.elemento.value('dfaTotal[1]','decimal(10,2)') [dfaTotal],
		nodo.elemento.value('dfaFechaIngreso[1]','varchar(10)') [dfaFechaIngreso]
	from @Factura_xml.nodes('Factura/DetalleFactura/Item') nodo(elemento)


end

alter procedure sp_GuardarClientes(
@Clientes_xml xml
)
as
begin
	

	insert into Clientes(cliNombre,cliCorreo,cliDireccion,cliFechaIngreso)
	select
		nodo.elemento.value('cliNombre[1]','varchar(50)') [cliNombre],
		nodo.elemento.value('cliCorreo[1]','varchar(50)') [cliCorreo],
		nodo.elemento.value('cliDireccion[1]','varchar(30)') [cliDireccion],
		nodo.elemento.value('cliFechaIngreso[1]','varchar(10)') [cliFechaIngreso]
	from @Clientes_xml.nodes('Clientes') nodo(elemento)


end

alter procedure sp_GuardarFotografos(
@Fotografos_xml xml
)
as
begin
	

	insert into Fotografos(dfoNombre,dfoFechaIngreso)
	select
		nodo.elemento.value('dfoNombre[1]','varchar(50)') [dfoNombre],
		nodo.elemento.value('dfoFechaIngreso[1]','varchar(10)') [dfoFechaIngreso]
		
	from @Fotografos_xml.nodes('Fotografos') nodo(elemento)


end
CREATE TABLE DetalleFactura(
	[dfaIdDetalleFactura] [int] IDENTITY(1,1) NOT NULL,
	[cliIdCliente] [int] NULL,
	[dfoIdFotografo] [int] NULL,
	[facIdFactura] [int] NULL,
	[dfaProducto] [varchar](50) NULL,
	[dfaPrecio] [decimal](10, 2) NULL,
	[dfaCantidad] [int] NULL,
	[dfaTotal] [decimal](10, 2) NULL,
	[dfaFechaIngreso] [varchar](10) NULL,
	)
CREATE TABLE Factura(
	[facIdFactura] [int] IDENTITY(1,1) NOT NULL,
	[facNumeroDocumento] [varchar](20) NULL,
	[facRazonSocial] [varchar](50) NULL,
	[facTotal] [decimal](10, 2) NULL,
	[facFechaIngreso] [varchar](10) NULL,
	)
CREATE TABLE Fotografos(
	[dfoIdFotografo] [int] IDENTITY(1,1) NOT NULL,
	[dfoNombre] [varchar](50) NULL,
	[dfoFechaIngreso] [varchar](10) NULL,
	)

CREATE TABLE Clientes(
	[cliIdCliente] [int] IDENTITY(1,1) NOT NULL,
	[cliNombre] [varchar](50) NULL,
	[cliCorreo] [varchar](50) NULL,
	[cliDireccion] [varchar](30) NULL,
	[cliFechaIngreso] [varchar](10) NULL,
)
create table Usuario(
usuId int primary key identity(1,1),
usuCorreo varchar(100),
usuClave varchar(500)
)

create procedure sp_RegistrarUsuario(
@usuCorreo varchar(100),
@usuClave varchar(500),
@usuRegistrado bit output,
@usuMensaje varchar(100) output
)
as
begin
if(not exists(select*from Usuario where usuCorreo=@usuCorreo))
begin
	insert into Usuario(usuCorreo,usuClave) values(@usuCorreo,@usuClave)
	set @usuRegistrado = 1 
	set @usuMensaje = 'usuario registrado'
	end
	else
	begin
		set @usuRegistrado = 0
		set @usuMensaje = 'correo ya existe'
	end
end

create procedure sp_ValidarUsuario(
@usuCorreo varchar(100),
@usuClave varchar(500)
)
as
begin
	if (exists(select * from Usuario where usuCorreo = @usuCorreo and usuClave = @usuClave))
		select usuId from Usuario where usuCorreo = @usuCorreo and usuClave = @usuClave 
	else
		select '0'
end

declare @usuRegistrado bit,@usuMensaje varchar(100)

exec sp_RegistrarUsuario 'pedro@gmail.com','5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', @usuRegistrado output, @usuMensaje output

select @usuRegistrado
select @usuMensaje

exec sp_ValidarUsuario 'pedro@gmail.com','5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5'

select * FROM Usuario