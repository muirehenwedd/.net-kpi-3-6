using Application.Common.Exceptions;
using Application.DeliveryQueryItems.Queries.GetDeliveryQuery;
using Application.Items.Commands.CreateItem;
using Application.Items.Commands.OrderItem;
using Application.Items.Commands.RefillItem;
using Application.Items.Queries.FindItem;
using Application.Items.Queries.GetAllItems;
using Application.Items.Queries.GetItem;
using ConsoleTables;
using Domain.Entities;
using MediatR;

namespace ConsoleUI;

public sealed class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IHostApplicationLifetime _hostLifetime;
    private readonly ISender _mediator;

    public Worker(ILogger<Worker> logger, IHostApplicationLifetime hostLifetime, ISender mediator)
    {
        _logger = logger;
        _hostLifetime = hostLifetime;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Welcome to the Warehouse management console application!");
        PrintHelp();

        while (!stoppingToken.IsCancellationRequested)
        {
            var consoleInput = Console.ReadLine();
            await HandleUserInput(consoleInput);
        }
    }

    private async Task HandleUserInput(string input)
    {
        var substrings = input.Split(' ', StringSplitOptions.TrimEntries ^ StringSplitOptions.RemoveEmptyEntries);

        switch (substrings)
        {
            case ["item", "create", _] when substrings[2].Length is >= 5 and <= 200:
                await CreateNewItem(substrings[2]);
                break;

            case ["item", "refill", _, _] when Guid.TryParse(substrings[2], out var id) &&
                                               int.TryParse(substrings[3], out var quantity):
                await Refill(id, quantity);
                break;

            case ["item", "order", _, _] when Guid.TryParse(substrings[2], out var id) &&
                                              int.TryParse(substrings[3], out var quantity):
                await Order(id, quantity);
                break;

            case ["item", "get", "all"]:
                await GetAllItems();
                break;

            case ["item", "get", _] when Guid.TryParse(substrings[2], out var id):
                await GetItemById(id);
                break;

            case ["item", "find", _]:
                await FindItem(substrings[2]);
                break;

            case ["item", "get", "query", ..]:
                await GetDeliveryQuery();
                break;

            case ["help"]:
                PrintHelp();
                break;

            case ["clear"]:
                Console.Clear();
                break;

            case ["exit" or "stop"]:
                _hostLifetime.StopApplication();
                break;

            case []:
                break;

            default:
                Console.WriteLine("bad input");
                break;
        }
    }

    private async Task CreateNewItem(string name)
    {
        var result = await _mediator.Send(new CreateItemCommand(name));

        Console.WriteLine(ItemToTable(result.Id, result.Name, result.Count));
    }

    private async Task Refill(Guid id, int quantity)
    {
        try
        {
            var result = await _mediator.Send(new RefillItemCommand(id, quantity));
            Console.WriteLine(ItemToTable(result.Id, result.Name, result.Count));
        }
        catch (NotFoundException)
        {
            Console.WriteLine("item not found");
        }
    }

    private async Task Order(Guid id, int quantity)
    {
        try
        {
            var result = await _mediator.Send(new OrderItemCommand(id, quantity));
            Console.WriteLine(ItemToTable(result.Id, result.Name, result.Count));
        }
        catch (NotFoundException)
        {
            Console.WriteLine("item not found");
        }
    }

    private async Task GetAllItems()
    {
        var result = await _mediator.Send(new GetAllItemsQuery());

        Console.WriteLine(
            ItemsToTable(result.Items
                .Select(i => (i.Id, i.Name, i.Count))));
    }

    private async Task GetItemById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetItemQuery(id));
            Console.WriteLine(ItemToTable(result.Id, result.Name, result.Count));
        }
        catch (NotFoundException)
        {
            Console.WriteLine("item not found");
        }
    }

    private async Task FindItem(string name)
    {
        try
        {
            var result = await _mediator.Send(new FindItemQuery(name));
            Console.WriteLine(ItemToTable(result.Id, result.Name, result.Count));
        }
        catch (NotFoundException)
        {
            Console.WriteLine("item not found");
        }
    }

    private async Task GetDeliveryQuery()
    {
        var result = await _mediator.Send(new GetDeliveryQueryQuery());

        var table = new ConsoleTable("Name", "Required quantity");

        foreach (var item in result.DeliveryQuery)
        {
            table.AddRow(item.ItemName, item.RequiredQuantity);
        }

        table.Configure(options => options.EnableCount = true);

        Console.WriteLine(table.ToStringAlternative());
    }

    private string ItemToTable(Guid id, string name, int count)
    {
        var table = new ConsoleTable("Id", "Name", "Count");

        table.AddRow(id, name, count);
        table.Configure(options => options.EnableCount = true);

        return table.ToStringAlternative();
    }

    private string ItemsToTable(IEnumerable<(Guid, string, int)> items)
    {
        var table = new ConsoleTable("Id", "Name", "Count");

        foreach (var tuple in items)
        {
            table.AddRow(tuple.Item1, tuple.Item2, tuple.Item3);
        }
        table.Configure(options => options.EnableCount = true);

        return table.ToStringAlternative();
    }

    private void PrintHelp()
    {
        Console.WriteLine("""
        item create [name] - create new item with specified name(from 5 to 50 characters)
        item refill [id] [quantity] - increase quantity of this item
        item order [id] [quantity] - decrease quantity of this item
        item get all
        item get [id] - get item
        item find [name] - get items with such name
        item get query - get delivery query
        help - display help
        clear - clear console
        exit, stop - stop application
        """);
    }
}