create database Cadastro1
go

use Cadastro1
go

drop table TblCliente
go

create table TblCliente
(IdCliente int primary key identity not null,
 Nome varchar(50),
 Sobrenome varchar(50),
 Email varchar(30),
 DataCadastro datetime,
 Ativo int)
 go

 select * from TblCliente
 go


 --Procedure de Listar
 create procedure spListarClientes
 as begin
 begin tran
 begin try

 select * from TblCliente
 order by Nome asc

 commit tran
 end try
 begin catch
 select ERROR_MESSAGE() as Retorno
 end catch
 end
 go

 drop procedure spInserirClientes
 --Procedure de inserir
 create procedure spInserirClientes
 @Nome varchar(50),
 @Sobrenome varchar(50),
 @Email varchar(30),
 @DataCadastro datetime,
 @Ativo int
 as begin
 begin tran
 begin try

 insert into TblCliente
 values(@Nome,@Sobrenome,@Email,@DataCadastro,@Ativo)

 commit tran
 end try
 begin catch
 select ERROR_MESSAGE() as Retorno
 end catch
 end
 go

 drop procedure spAlterarClientes
 --Procedure de Alterar
 create procedure spAlterarClientes
 @IdCliente int,
 @Nome varchar(50),
 @Sobrenome varchar(50),
 @Email varchar(30),
 @DataCadastro datetime,
 @Ativo int
 as begin
 begin tran
 begin try

 update TblCliente 
 set Nome=@Nome,Sobrenome=@Sobrenome,Email=@Email,
 DataCadastro=@DataCadastro,Ativo=@Ativo
 where IdCliente = @IdCliente

 commit tran
 end try
 begin catch
 select ERROR_MESSAGE() as Retorno
 end catch
 end
 go

 --Procedure p/ Excluir
 create procedure spExcluirCliente
 @IdCliente int
 as begin
 begin tran
 begin try

 delete from TblCliente
 where IdCliente = @IdCliente

 commit tran
 end try
 begin catch
 select ERROR_MESSAGE() as Retorno
 end catch
 end
 go

 --Procedure p/ selecionar o nome do cliente
 create procedure spSelecionarClienteNome
 @Nome varchar(50)
 as begin
 begin tran
 begin try

 select Nome from TblCliente
 where Nome = @Nome

 commit tran
 end try
 begin catch
 select ERROR_MESSAGE() as Retorno
 end catch
 end
 go

 /*---------------------------------------*/
 drop table TblProduto


 create table TblProduto
 (IdProduto int primary key identity not null,
  Nome varchar(50),
  Valor int,
  Disponivel int,
  Id_Cliente int)
  go

  insert into TblProduto values('Cerveja Lata',8.00,1,1)

  alter table TblProduto add constraint FK_Produto_Cliente
  foreign key(Id_Cliente) references TblCliente(IdCliente)
  go

  --Procedure de Listar Produtos
  create procedure spListarProdutos
  as begin
 begin tran
 begin try

 select p.IdProduto,
        p.Nome,
		p.Valor,
		p.Disponivel,
		c.Nome as Cliente
		from TblCliente c inner join TblProduto p
		on c.IdCliente = p.Id_Cliente
		order by c.Nome asc

 commit tran
 end try
 begin catch
 select ERROR_MESSAGE() as Retorno
 end catch
 end
 go


 --Procedure de inserir
 create procedure spInserirProdutos
 @Nome varchar(50),
 @Valor int,
 @Disponivel int,
 @Id_Cliente int
  as begin
 begin tran
 begin try

 insert into TblProduto
 values(@Nome,@Valor,@Disponivel,@Id_Cliente)

  commit tran
 end try
 begin catch
 select ERROR_MESSAGE() as Retorno
 end catch
 end
 go

 --Procedure p/ Excluir
 create procedure spExcluirProduto
 @IdProduto int
 as begin
 begin tran
 begin try

 delete from TblProduto
 where IdProduto = @IdProduto

 commit tran
 end try
 begin catch
 select ERROR_MESSAGE() as Retorno
 end catch
 end
 go

 --Procedure p/ selecionar o nome do produto
 create procedure spSelecionarProdutoNome
 @Nome varchar(50)
 as begin
 begin tran
 begin try

 select Nome from TblProduto
 where Nome = @Nome

 commit tran
 end try
 begin catch
 select ERROR_MESSAGE() as Retorno
 end catch
 end
 go

 create procedure spAlterarProdutos
 @IdProduto int,
 @Nome varchar(50),
 @Valor int,
 @Disponivel int,
 @Id_Cliente int
 
 as begin
 begin tran
 begin try

 update TblProduto
 set Nome=@Nome,Valor=@Valor,Disponivel=@Disponivel,
 Id_Cliente=@Id_Cliente
 where IdProduto = @IdProduto

 commit tran
 end try
 begin catch
 select ERROR_MESSAGE() as Retorno
 end catch
 end
 go