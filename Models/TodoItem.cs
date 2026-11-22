namespace LifeManagementApp.Models;

// 待办条目：五个字段 —— 评分表 §1 的最低要求
// 存进 SQLite（EF 直接映射这些属性）
public class TodoItem
{
    public int Id { get; set; }                       // 主键，EF 自动生成
    public string Title { get; set; } = "";           // 标题（空标题将被拒绝）
    public int Priority { get; set; } = 3;            // 1(最高)–5(最低)
    public DateTime DueDate { get; set; } = DateTime.Today;
    public bool IsDone { get; set; }                  // 完成与否
}
