DROP DATABASE IF EXISTS `pretrip`;
CREATE DATABASE IF NOT EXISTS `pretrip`;
USE `pretrip`;

CREATE TABLE `Conta` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Saldo` double(20,2),
  PRIMARY KEY (`Id`)
);

CREATE TABLE `Endereco` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Numero` int(11),
  `Complemento` int(11),
  `Rua` varchar(255),
  `Bairro` varchar(150),
  `Cidade` varchar(150),
  `Estado` varchar(150),
  PRIMARY KEY (`Id`)
);

CREATE TABLE `Empresa` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Cnpj` char(14),
  `RazaoSocial` varchar(255),
  `NomeFantasia` varchar(255),
  `IdEndereco` int(11),
  PRIMARY KEY (`Id`),
  CONSTRAINT `fk_empresa_endereco` FOREIGN KEY (`IdEndereco`) REFERENCES `Endereco` (`Id`)
);

CREATE TABLE `Evento` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(255),
  `Tags` varchar(255),
  PRIMARY KEY (`Id`)
);

CREATE TABLE `Veiculo` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Modelo` varchar(255),
  `Placa` varchar(8),
  `Tipo` varchar(100),
  `QuantidadeLugares` int(11),
  PRIMARY KEY (`Id`)
);

CREATE TABLE `Viagem` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `IdVeiculo` int(11),
  `IdEmpresa` int(11),
  `PrecoPassagem` double,
  `DescricaoViagem` varchar(45),
  `IdEnderecoOrigem` int(11),
  `IdEnderecoDestino` int(11),
  `DataHrSaida` datetime,
  `DataHrChegada` datetime,
  `QuantidadeLugaresDisponiveis` int(11),
  PRIMARY KEY (`Id`),
  CONSTRAINT `fk_viagem_veiculo` FOREIGN KEY (`IdVeiculo`) REFERENCES `Veiculo` (`Id`),
  CONSTRAINT `fk_viagem_empresa` FOREIGN KEY (`IdEmpresa`) REFERENCES `Empresa` (`Id`),
  CONSTRAINT `fk_viagem_endOrigem` FOREIGN KEY (`IdEnderecoOrigem`) REFERENCES `Endereco` (`Id`),
  CONSTRAINT `fk_viagem_endDestino` FOREIGN KEY (`IdEnderecoDestino`) REFERENCES `Endereco` (`Id`)
);

CREATE TABLE `Pedido` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `IdViagem` int(11),
  `DataRealizacao` datetime,
  `Quantidade` int(11),
  `PrecoTotal` double,
  PRIMARY KEY (`Id`),
  CONSTRAINT `fk_pedido_viagem` FOREIGN KEY (`IdViagem`) REFERENCES `Viagem` (`Id`)
);

CREATE TABLE `Pessoa` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(255),
  `Telefone` int(11),
  `Cpf` int(11) NOT NULL,
  `DataNascimento` date,
  `IdConta` int(11),
  PRIMARY KEY (`Id`),
  CONSTRAINT `fk_conta_pessoa` FOREIGN KEY (`IdConta`) REFERENCES `Conta` (`Id`)
);

CREATE TABLE `Usuario` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Login` varchar(20) NOT NULL,
  `Senha` varchar(20) DEFAULT NULL,
  `IsAdmin` tinyint(1) DEFAULT NULL,
  `IdPessoa` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `fk_usuario_pessoa` FOREIGN KEY (`IdPessoa`) REFERENCES `Pessoa` (`Id`)
);

CREATE TABLE `usuario_pedido` (
  `IdUsuario` int(11) NOT NULL,
  `IdPedido` int(11) NOT NULL,
  PRIMARY KEY (`IdUsuario`,`IdPedido`),
  CONSTRAINT `fk_usuarioPedido_usuario` FOREIGN KEY (`IdUsuario`) REFERENCES `Usuario` (`Id`),
  CONSTRAINT `fk_usuarioPedido_pedido` FOREIGN KEY (`IdPedido`) REFERENCES `Pedido` (`Id`)
);

CREATE TABLE `usuario_viagem` (
  `IdUsuario` int(11) NOT NULL,
  `IdViagem` int(11) NOT NULL,
  PRIMARY KEY (`IdUsuario`,`IdViagem`),
  CONSTRAINT `fk_usuarioViagem_usuario` FOREIGN KEY (`IdUsuario`) REFERENCES `Usuario` (`Id`),
  CONSTRAINT `fk_usuarioViagem_viagem` FOREIGN KEY (`IdViagem`) REFERENCES `Viagem` (`Id`)
);

CREATE TABLE `viagem_evento` (
  `IdViagem` int(11) NOT NULL,
  `IdEvento` int(11) NOT NULL,
  PRIMARY KEY (`IdViagem`,`IdEvento`),
  CONSTRAINT `fk_viagem_viagemEvento` FOREIGN KEY (`IdViagem`) REFERENCES `Viagem` (`Id`),
  CONSTRAINT `fk_viagemEvento_evento` FOREIGN KEY (`IdEvento`) REFERENCES `Evento` (`Id`)
);
