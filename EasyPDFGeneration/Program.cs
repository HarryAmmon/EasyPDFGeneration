using EasyPDFGeneration.Models;
using QuestPDF.Elements.Table;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Schema;

QuestPDF.Settings.License = LicenseType.Community;


var funds = new List<Fund>
{
    new Fund{ Description = "My fund description 1", FundAmount = 1234.32M, FundCreationDate = new DateTime(2023, 10, 1), Name = "My super fund" },
    new Fund{ Description = "My fund description 2", FundAmount = 1234000.32M, FundCreationDate = new DateTime(1999, 3, 10), Name = "My great fund" },
    new Fund{ Description = "My fund description 3", FundAmount = 12341234.32M, FundCreationDate = new DateTime(2019, 8, 6), Name = "My epic fund" },
    new Fund{ Description = "My fund description 1", FundAmount = 1234.32M, FundCreationDate = new DateTime(2023, 10, 1), Name = "My super fund" },
    new Fund{ Description = "My fund description 2", FundAmount = 1234000.32M, FundCreationDate = new DateTime(1999, 3, 10), Name = "My great fund" },
    new Fund{ Description = "My fund description 3", FundAmount = 12341234.32M, FundCreationDate = new DateTime(2019, 8, 6), Name = "My epic fund" },
    new Fund{ Description = "My fund description 1", FundAmount = 1234.32M, FundCreationDate = new DateTime(2023, 10, 1), Name = "My super fund" },
    new Fund{ Description = "My fund description 2", FundAmount = 1234000.32M, FundCreationDate = new DateTime(1999, 3, 10), Name = "My great fund" },
    new Fund{ Description = "My fund description 3", FundAmount = 12341234.32M, FundCreationDate = new DateTime(2019, 8, 6), Name = "My epic fund" },
    new Fund{ Description = "My fund description 1", FundAmount = 1234.32M, FundCreationDate = new DateTime(2023, 10, 1), Name = "My super fund" },
    new Fund{ Description = "My fund description 2", FundAmount = 1234000.32M, FundCreationDate = new DateTime(1999, 3, 10), Name = "My great fund" },
    new Fund{ Description = "My fund description 3", FundAmount = 12341234.32M, FundCreationDate = new DateTime(2019, 8, 6), Name = "My epic fund" },
    new Fund{ Description = "My fund description 1", FundAmount = 1234.32M, FundCreationDate = new DateTime(2023, 10, 1), Name = "My super fund" },
    new Fund{ Description = "My fund description 2", FundAmount = 1234000.32M, FundCreationDate = new DateTime(1999, 3, 10), Name = "My great fund" },
    new Fund{ Description = "My fund description 3", FundAmount = 12341234.32M, FundCreationDate = new DateTime(2019, 8, 6), Name = "My epic fund" },
    new Fund{ Description = "My fund description 1", FundAmount = 1234.32M, FundCreationDate = new DateTime(2023, 10, 1), Name = "My super fund" },
    new Fund{ Description = "My fund description 2", FundAmount = 1234000.32M, FundCreationDate = new DateTime(1999, 3, 10), Name = "My great fund" },
    new Fund{ Description = "My fund description 3", FundAmount = 12341234.32M, FundCreationDate = new DateTime(2019, 8, 6), Name = "My epic fund" },
};



var document = new SimpleTableDocument(funds);


document.ShowInPreviewer();
document.GeneratePdf("MySuperSimplePDF.pdf");



public class SimpleTableDocument : IDocument
{
    private readonly IEnumerable<Fund> _funds;
    public SimpleTableDocument(IEnumerable<Fund> funds)
    {
        _funds = funds;
    }
    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);

                page.Header().Column(column =>
                {
                    column.Item().ShowOnce().PaddingTop(11, Unit.Centimetre).PaddingBottom(11, Unit.Centimetre).Text("This is my title page").SemiBold().FontSize(30).FontColor(Colors.Red.Medium);
                    column.Item().SkipOnce().Text("This is the heading on every other page").SemiBold().FontSize(30).FontColor(Colors.Red.Medium);

                });

                page.Content().SkipOnce().Element(ComposeContent);
                page.Content().Element(ComposeContent);

            });
    }

    public void ComposeContent(IContainer container)
    {

        container.Column(column =>
        {
            column.Item().Element(ComposeTable);
        });
    }


    public void ComposeTable(IContainer container)
    {
        container.Table(table =>
        {
            // step 1
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            // step 2
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("Fund name");
                header.Cell().Element(CellStyle).Text("Amount");
                header.Cell().Element(CellStyle).Text("Description");
                header.Cell().Element(CellStyle).Text("Date");
                header.Cell().Element(CellStyle).Text("Fund size");

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });


            //step 3
            foreach (var fund in _funds)
            {
                table.Cell().Element(CellStyle).Text(fund.Name);
                table.Cell().Element(CellStyle).AlignRight().Text($"£{fund.FundAmount}");
                table.Cell().Element(CellStyle).AlignRight().Text(fund.Description);
                table.Cell().Element(CellStyle).AlignRight().Text($"{fund.FundCreationDate.ToShortDateString()}");
                table.GetFundSize(fund, CellStyle);





                static IContainer CellStyle(IContainer container)
                {
                    return container
                        .BorderLeft(1)
                        .BorderRight(1)
                        .BorderTop(1)
                        .BorderBottom(1)
                        .BorderColor(Colors.Black)
                        .PaddingVertical(5);
                }
            }

        });

    }



}

public static class CellGenerator
{
    public static void GetFundSize(this TableDescriptor table, Fund fund, Func<IContainer, IContainer> cellStyle)
    {
        var condition = fund.FundAmount > 100000;

        var cell = table.Cell().Element(cellStyle);
        if (condition)
        {
            cell.Text("Large");
        }
        else
        {
            cell.Text("Small");
        }

    }
}