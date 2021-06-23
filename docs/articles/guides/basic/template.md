# Template

In this part we consider how to load matrix to file such as `.html`, `.md`.

First, `Template` is abstract class which intended for base functionality open and write of file.

## TemplateMarkdown
`TemplateMarkdown` is inherited class from `Template`, this class works with Markdown file.

Let's consider how to use `TemplateMarkdown`.

```c#
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Options;
...
public async void BinaryWriteMatrix()
{
    var matrix = BuildMatrix.BuildRandom<int>(15, 12);
    var html = new TemplateMarkdown(title: "MatrixDotNet");
    
    await html.BinarySaveAsync(matrix);
}
```

As you can see `BinarySaveAsync` uses for saving you matrix to file, this file saves in `.dat` format and located in `bin/Release/MatrixLogs` or `bin/Debug/MatrixLogs` folder of your project.

Next sample displays how to load matrix from `.dat` file

```c#
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Options;
...
public async void BinaryReadMatrix()
{
    var html = new TemplateMarkdown(title: "MatrixDotNet");
    var resultMatrix = await html.BinaryOpenAsync<int>();
}
```

## TemplateHtml
`TemplateHtml` also inherited class from `Template` and works with HTML file.

Let's consider how to use `TemplateHtml`.

```c#
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Options;
...
public async void BinaryWriteReadMatrix()
{
    var matrix = BuildMatrix.BuildRandom<int>(15, 15);
    var html = new TemplateHtml(title: "MatrixDotNet");
    
    await html.BinarySaveAsync(matrix);
    var resultMatrix = await html.BinaryOpenAsync<int>();
}
```

As you can see `TemplateHtml` does the same thing as `TemplateMarkdown`.

> [!NOTE]
> If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).