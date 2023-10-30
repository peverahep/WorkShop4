# АНАЛИЗ ДАННЫХ И ИСКУССТВЕННЫЙ ИНТЕЛЛЕКТ [in GameDev]
Отчет по лабораторной работе #4 выполнил(а):
- Мальцев Богдан Андреевич
- ФО-220005
Отметка о выполнении заданий (заполняется студентом):

| Задание | Выполнение | Баллы |
| ------ | ------ | ------ |
| Задание 1 | * | 60 |
| Задание 2 | * | 20 |
| Задание 3 | * | 20 |

знак "*" - задание выполнено; знак "#" - задание не выполнено;

Работу проверили:
- к.т.н., доцент Денисов Д.В.
- к.э.н., доцент Панов М.А.
- ст. преп., Фадеев В.О.

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Структура отчета

- Данные о работе: название работы, фио, группа, выполненные задания.
- Цель работы.
- Задание 1.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 2.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 3.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Выводы.
- ✨Magic ✨

## Цель работы
разработать и обучить работающую модель перцептрона

Ход работы:

## Задание 1
### в проекте Unity реализовать перцептрон, который умеет производить вычисления:
### OR | дать комментарии о корректности работы
### AND | дать комментарии о корректности работы
### NAND | дать комментарии о корректности работы
### XOR | дать комментарии о корректности работы

OR - Тренировка прошла успешно, работа является корректной. Тренировка перцептрона на данный логический элемент требует меньше всего эпох

AND - Тренировка прошла успешно, работа является корректной. Тренировка перцептрона на данный логический элемент требует средних усилий

NAND - Тренировка прошла успешно, работа является корректной. Тренировка перцептрона на данный логический элемент требует средних усилий, осваивается чуть легче, чем AND 

XOR - Выполнить тренировку перцептрона на данный логический элемент не получилось. Работа является некорректной.

## Задание 2
### Построить графики зависимости количества эпох от ошибки  обучения. Указать от чего зависит необходимое количество эпох обучения.

Добавим в проект Unity следующий скрипт:

```cs
using System.Collections;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Dynamic;

namespace GoogleSheetsHelper
{
    public class GoogleSheetsHelper
    {
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "GoogleSheetsHelper";

        private readonly SheetsService _sheetsService;
        private readonly string _spreadsheetId;

        public GoogleSheetsHelper(string credentialFileName, string spreadsheetId)
        {
            var credential = GoogleCredential.FromStream(new FileStream(credentialFileName, FileMode.Open)).CreateScoped(Scopes);

            _sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            _spreadsheetId = spreadsheetId;
        }

        public void AddCells(GoogleSheetParameters googleSheetParameters, List<GoogleSheetRow> rows)
        {
            var requests = new BatchUpdateSpreadsheetRequest { Requests = new List<Request>() };

            var sheetId = GetSheetId(_sheetsService, _spreadsheetId, googleSheetParameters.SheetName);

            GridCoordinate gc = new GridCoordinate
            {
                ColumnIndex = googleSheetParameters.RangeColumnStart - 1,
                RowIndex = googleSheetParameters.RangeRowStart - 1,
                SheetId = sheetId
            };

            var request = new Request { UpdateCells = new UpdateCellsRequest { Start = gc, Fields = "*" } };

            var listRowData = new List<RowData>();

            foreach (var row in rows)
            {
                var rowData = new RowData();
                var listCellData = new List<CellData>();
                foreach (var cell in row.Cells)
                {
                    var cellData = new CellData();
                    var extendedValue = new ExtendedValue { StringValue = cell.CellValue };

                    cellData.UserEnteredValue = extendedValue;
                    listCellData.Add(cellData);
                }
                rowData.Values = listCellData;
                listRowData.Add(rowData);
            }
            request.UpdateCells.Rows = listRowData;

            // It's a batch request so you can create more than one request and send them all in one batch. Just use reqs.Requests.Add() to add additional requests for the same spreadsheet
            requests.Requests.Add(request);

            _sheetsService.Spreadsheets.BatchUpdate(requests, _spreadsheetId).Execute();
        }

        private string GetColumnName(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];
            return value;
        }

        private GoogleSheetParameters MakeGoogleSheetDataRangeColumnsZeroBased(GoogleSheetParameters googleSheetParameters)
        {
            googleSheetParameters.RangeColumnStart = googleSheetParameters.RangeColumnStart - 1;
            googleSheetParameters.RangeColumnEnd = googleSheetParameters.RangeColumnEnd - 1;
            return googleSheetParameters;
        }

        private int GetSheetId(SheetsService service, string spreadSheetId, string spreadSheetName)
        {
            var spreadsheet = service.Spreadsheets.Get(spreadSheetId).Execute();
            var sheet = spreadsheet.Sheets.FirstOrDefault(s => s.Properties.Title == spreadSheetName);
            int sheetId = (int)sheet.Properties.SheetId;
            return sheetId;
        }
    }

    public class GoogleSheetCell
    {
        public string CellValue { get; set; }
    }

    public class GoogleSheetParameters
    {
        public int RangeColumnStart { get; set; }
        public int RangeRowStart { get; set; }
        public int RangeColumnEnd { get; set; }
        public int RangeRowEnd { get; set; }
        public string SheetName { get; set; }
        public bool FirstRowIsHeaders { get; set; }
    }

    public class GoogleSheetRow
    {
        public GoogleSheetRow() => Cells = new List<GoogleSheetCell>();

        public List<GoogleSheetCell> Cells { get; set; }
    }
}
```

А также модифицируем метод "Start" перцептрона следующим образом:
```cs
    void Start()
    {
        var gsh = new GoogleSheetsHelper.GoogleSheetsHelper("Assets\\script\\peveraheper-7866f5527595.json", "1SKT2q9BbHb1h2-URG-Mu6uWvy49APmT8DYRh7myDqSU");
        var row1 = new GoogleSheetRow();
        var row2 = new GoogleSheetRow();
        row2.Cells.AddRange(new List<GoogleSheetCell>() { new GoogleSheetCell() { CellValue = "Train number" }, new GoogleSheetCell() { CellValue = "ErrorCount" } });
        row1.Cells.Add(new GoogleSheetCell() { CellValue = Name });
        var rows = new List<GoogleSheetRow>() { row1, row2 };
        for (int i = 1; i <= 20; i++)
        {
            rows.Add(new GoogleSheetRow());
            rows[^1].Cells.Add(new GoogleSheetCell() { CellValue = i.ToString() });
            Train(i);
            rows[^1].Cells.Add(new GoogleSheetCell() { CellValue = totalError.ToString() });
            if (totalError == 0) break;
        }
        Debug.Log($"{Name}:");
        Debug.Log("Test 0 0: " + CalcOutput(0, 0));
        Debug.Log("Test 0 1: " + CalcOutput(0, 1));
        Debug.Log("Test 1 0: " + CalcOutput(1, 0));
        Debug.Log("Test 1 1: " + CalcOutput(1, 1));

        gsh.AddCells(new GoogleSheetParameters() { SheetName = Name, RangeColumnStart = 1, RangeRowStart = 1 }, rows);
    }
```

В этом случае получаем гугл-таблицу в которой мы можем построить график зависимости эпох от ошибки обучения: https://docs.google.com/spreadsheets/d/1SKT2q9BbHb1h2-URG-Mu6uWvy49APmT8DYRh7myDqSU/edit#gid=0

Необходимое количество эпох зависит от сложности изучаемого элемента, и потенциальной возможности обучение перцептрона на данный элемент.


## Задание 3
### Построить визуальную модель работы перцептрона на сцене Unity. //можно использовать GameObject Сube и метод OnCollisionEnter.

С визуальной сценой подробнее можно ознакомиться скачав данный репозиторий


## Выводы

В ходе лабораторной работы были предприняты попытки обучения перцептрона поведению логических элементов. Обучение прошло успешно за исключением элемента "XOR".
Данный результат является ожидаемым, так как он является подтверждением Xor-проблемы, которую ранее описал Minsky.
## Powered by

**BigDigital Team: Denisov | Fadeev | Panov**
