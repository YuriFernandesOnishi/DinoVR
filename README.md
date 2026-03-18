# DinoVR

Simulação em realidade virtual desenvolvida em **Unity XR** como derivação experimental do projeto **VirTEAi**, utilizando dinossauros como elemento lúdico para atividades de vida diária em ambientes imersivos.

## Sobre o projeto

O DinoVR explora uma abordagem interativa em realidade virtual onde elementos visuais de dinossauros são usados como mediadores de atenção e engajamento em tarefas cotidianas.

A proposta nasce como uma simulação técnica do conceito central do VirTEAi: construir ambientes controlados capazes de registrar comportamento espacial, atenção visual e padrões de interação em contextos imersivos.

## Objetivo atual

Nesta fase inicial, o foco está em validar a infraestrutura de captura e processamento de dados dentro do ambiente XR.

Atualmente o projeto já possui:

* movimentação XR implementada
* captura de head tracking em tempo real
* registro periódico de posição e rotação da câmera
* serialização dos dados em JSON
* envio para API externa para processamento

## Arquitetura atual

O sistema realiza a coleta da transformação da câmera principal em intervalos regulares.

Cada amostra registra:

* posição X, Y, Z
* rotação quaternion X, Y, Z, W
* timestamp da captura

Os dados são agrupados em memória e enviados para uma API HTTP para análise posterior.

## Fluxo atual de captura

* tecla **I** inicia gravação
* tecla **O** encerra captura
* os dados são enviados em JSON para a API

## Estrutura dos dados enviados

```json
{
  "samples": [
    {
      "px": 0.0,
      "py": 1.6,
      "pz": 0.2,
      "rx": 0.0,
      "ry": 0.1,
      "rz": 0.0,
      "rw": 1.0,
      "time": 12.5
    }
  ]
}
```

## Stack utilizada

* Unity
* C#
* XR Interaction Toolkit
* Unity Input System
* HTTP API Integration

## Próximos passos

* implementação de interações contextuais com objetos
* cenários de atividades da vida diária
* eventos comportamentais no ambiente
* integração com métricas adicionais de atenção
* expansão da análise de dados em backend

## Relação com VirTEAi

O DinoVR funciona como ambiente experimental para validar componentes reutilizáveis do ecossistema VirTEAi, principalmente:

* captura comportamental
* arquitetura modular
* integração entre XR e backend analítico

## Status

Projeto em desenvolvimento.
