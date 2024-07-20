# Controlador Agendamento

Projeto MVP para hackathon da FIAP Pos-Tech Software Architecture.

## Grupo 6

- RM350836 (3SOAT): Marcio Lages Silva - marciolages@msn.com
- RM351061 (3SOAT): Renan Silva Xavier - renansx2013@hotmail.com
- RM351631 (3SOAT): Victor Sadao Higa Nagahara - viih.higa@gmail.com
- RM351041 (3SOAT): Vitor de Souza - vitordesolza@gmail.com

## Contexto do Negócio

A Health&Med, uma startup inovadora no setor de saúde, está desenvolvendo um novo sistema que irá revolucionar a Telemedicina no país. 
Atualmente, a startup oferece a possibilidade de agendamento de consultas e realização de consultas online (Telemedicina) por meio de sistemas terceiros como Google Agenda e Google Meetings.
Recentemente, a empresa recebeu um aporte e decidiu investir no desenvolvimento de um sistema proprietário, visando proporcionar um serviço de maior qualidade, segurança dos dados dos pacientes e redução de custos. 
O objetivo é criar um sistema robusto, escalável e seguro que permita o gerenciamento eficiente desses agendamentos e consultas.
Além de conter as funcionalidades de agendamento e realização de consultas online, o sistema terá o diferencial de uma nova funcionalidade: o Prontuário Eletrônico. 
O Prontuário Eletrônico permitirá o armazenamento e compartilhamento de documentos, exames, cartão de vacinas, e outros registros médicos entre as partes envolvidas, garantindo maior assertividade nos diagnósticos.
Para viabilizar o desenvolvimento de um sistema que esteja em conformidade com as melhores práticas de qualidade e arquitetura de software, a Health&Med contratou os alunos do curso (SOAT) para fazer a análise do projeto e a arquitetura do software.

## Como rodar o projeto

1. Clone o repositório
2. Acesse a pasta do projeto
3. Execute o comando `docker-compose up --build`
4. Acesse a URL `http://localhost:5005/swagger/index.html`

## Tecnologias utilizadas
- ASP.NET Core 8.0

## Autenticação
É possível gerar um token JWT para autenticação, para isso é necessário rodar o comando abaixo:

```bash
dotnet user-jwts create
```
