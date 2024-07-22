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

## Como fazer deploy AWS
- Rodar o Github Action: *Build and Deploy AWS* localizado na pasta `.github/workflows/release-aws.yml`

## Tecnologias utilizadas
- ASP.NET Core 8.0

## Documentação

- Cadastro:
```bash
curl -X 'POST' \
  'http://localhost:5005/cadastrar' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "crm": "string",
  "senha": "string",
  "email": "string",
  "cpf": "string",
  "tipo": 0
}'
```

- Login
```bash
curl -X 'POST' \
  'http://localhost:5005/login' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "crm": "string",
  "senha": "string",
  "email": "string",
  "cpf": "string",
  "tipo": 0
}'
```

- Cadastro Médico
```bash
curl -X 'POST' \
  'http://localhost:5005/medico' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer <Token>' \
  -H 'Content-Type: application/json' \
  -d '{
  "nome": "string",
  "crm": "string",
  "especialidade": "string"
}'
```

- Cadastro Paciente
```bash
curl -X 'POST' \
  'http://localhost:5005/paciente' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer <Token>' \
  -H 'Content-Type: application/json' \
  -d '{
  "nome": "string",
  "cpf": "string",
  "email": "string",
  "telefone": "string"
}'
```

- Cadastro Arquivo
```bash
curl -X 'POST' \
  'http://localhost:5005/arquivo' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer <Token>' \
  -H 'Content-Type: application/json' \
  -d '{
  "nome": "string",
  "url": "string",
  "acessivel": true,
  "epiracaoAcesso": "2024-07-21T19:26:04.568Z"
}'
```

- Cadastro Prontuário
```bash
curl -X 'POST' \
  'http://localhost:5005/prontuario' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer <Token>' \
  -H 'Content-Type: application/json' \
  -d '{
  "pacienteId": "5e411a96-0a70-499c-841c-69d115402be8",
  "arquivosIds": [
    "d515fba7-04f2-4cfd-99ab-a3c279ef9363"
  ]
}'
```

- Cadastro Agenda
```bash
curl -X 'POST' \
  'http://localhost:5005/agenda' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer <Token>' \
  -H 'Content-Type: application/json' \
  -d '{
  "medicoId": "1155fdf4-f708-4c94-ab71-12bbc8c2f79e",
  "horariosIds": [
    
  ]
}'
```

- Cadastro Horário
```bash
curl -X 'POST' \
  'http://localhost:5005/horario' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer <Token>' \
  -H 'Content-Type: application/json' \
  -d '{
  "data": "2024-07-21T19:26:56.355Z",
  "agendaId": "9c6a5f11-c8f2-48e5-9326-8094ab70f99d"
}'
```

- Cadastro Consulta
```bash
curl -X 'POST' \
  'http://localhost:5005/consulta' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer <Token>' \
  -H 'Content-Type: application/json' \
  -d '{
  "pacienteId": "5e411a96-0a70-499c-841c-69d115402be8",
  "horaId": "365b0273-5b98-46ab-ae39-1cf76ee7cd51",
  "prontuarioId": "99a7d5d8-f17e-42ed-876b-0f806f417ce2",
  "estado": 0
}'
```

- Confirmar Consulta
```bash
curl -X 'PATCH' \
  'http://localhost:5005/consulta/2444bf84-b346-4cb7-9b25-95e256e18550/estado?estado=0' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer <Token>'
```
