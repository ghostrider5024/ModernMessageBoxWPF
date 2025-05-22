# 💬 Modern MessageBox for WPF (No MVVM Required)

Hộp thoại hiện đại, async/await hỗ trợ hiển thị từ bất kỳ đâu trong WPF mà không cần MVVM.  
Thiết kế gọn gàng, dễ tích hợp, có thể thay theme và tuỳ biến biểu tượng linh hoạt.

---

## 🎨 Tính năng nổi bật

- ✨ UI hiện đại, tối giản
- 🔄 Hỗ trợ `async/await`
- ⚠️ Hỗ trợ nhiều kiểu thông báo: Thông tin, Cảnh báo, Lỗi, Xác nhận
- 🧠 Không cần MVVM — gọi thẳng từ `code-behind`
- 🌈 Dễ tuỳ chỉnh theme qua `ResourceDictionary`

---

## ⚙️ Cài đặt và tích hợp

### 1. Thêm Resource vào App.xaml

````xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/ModernMessageBoxWPF;component/Themes/Light.xaml" />
        </ResourceDictionary.MergedDictionaries>
        <modernMessageBox:ModernMessgeBoxConverter x:Key="DialogIconConverter" />
    </ResourceDictionary>
</Application.Resources>

### 2. Show MessageBox bằng
```csharp
public class HelloWorld
{
    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        var cancelButtonStyle =FindResource("CancelButtonStyle") as Style;
        var result = await ModernMessageBoxManager
            .ShowDialogAsync(this
            , "Xác nhận"
            , "Bạn muốn thoát ứng dụng?"
            , DialogType.Warning, new ModernMessageBoxWPF.Models.ModernMessageBoxStyle()
            {
                CancelButtonStyle = cancelButtonStyle
            });

        if (result)
        {
            Application.Current.Shutdown();
        }
    }
}
````

## 🧩 Thuộc tính (Dependency Properties)

| Tên Property           | Kiểu dữ liệu                        | Mặc định       | Mô tả                                              |
| ---------------------- | ----------------------------------- | -------------- | -------------------------------------------------- |
| `AnimationType`        | `ModernMessageBoxAnimationTypeEnum` | `SlideFromTop` | Kiểu hiệu ứng khi hiển thị và ẩn message box.      |
| `AnimationInDuration`  | `TimeSpan`                          | `00:00:00.250` | Thời gian chạy animation khi hiển thị message box. |
| `AnimationOutDuration` | `TimeSpan`                          | `00:00:00.250` | Thời gian chạy animation khi đóng message box.     |
| `CancelButtonStyle`    | `Style`                             | `00:00:00.250` | Thời gian chạy animation khi đóng message box.     |
| `AnimationOutDuration` | `TimeSpan`                          | `00:00:00.250` | Thời gian chạy animation khi đóng message box.     |

## ⚙️ Giá trị enum ModernMessageBoxAnimationTypeEnum

| Enum Value        | Mô tả                                |
| ----------------- | ------------------------------------ |
| `Fade`            | Hiệu ứng mờ dần vào/ra               |
| `Zoom`            | Hiệu ứng phóng to/thu nhỏ            |
| `SlideFromBottom` | Trượt lên từ phía dưới               |
| `SlideFromTop`    | Trượt xuống từ phía trên _(default)_ |
| `SlideFromLeft`   | Trượt vào từ bên trái                |
| `SlideFromRight`  | Trượt vào từ bên phải                |
| `Flip`            | Hiệu ứng lật dạng 3D                 |

## 🎨 Resource Dictionary - Giao diện Modern cho MessageBox

File `ResourceDictionary.xaml` này chứa tập hợp các brush và màu sắc được sử dụng cho MessageBox kiểu hiện đại. Bạn có thể tái sử dụng các resource này cho bất kỳ nút, hộp thoại, hay thành phần giao diện nào để đảm bảo giao diện nhất quán.

## 🧱 Danh sách các resource

| **Key**                      | **Loại**          | **Ý nghĩa / Mục đích sử dụng**                                                        |
| ---------------------------- | ----------------- | ------------------------------------------------------------------------------------- |
| `ModernMessageBoxBackground` | `SolidColorBrush` | Màu nền chính cho MessageBox. Mặc định là `white`.                                    |
| `PrimaryBg`                  | `SolidColorBrush` | Màu chủ đạo được dùng cho nền của các nút hoặc vùng chính.                            |
| `PrimaryBg_Color`            | `Color`           | Giá trị màu `Color` tương ứng với `PrimaryBg`, dùng cho animation hoặc các converter. |
| `PrimaryText`                | `SolidColorBrush` | Màu chữ đặt trên nền chủ đạo. Mặc định là `White`.                                    |
| `PrimaryText_Color`          | `Color`           | Phiên bản `Color` của `PrimaryText`.                                                  |
| `HoveredPrimaryBg_Color`     | `Color`           | Màu nền khi người dùng hover chuột vào nút.                                           |
| `PressedBg`                  | `SolidColorBrush` | Màu nền khi nút được nhấn.                                                            |
| `PressedBg_Color`            | `Color`           | Giá trị `Color` tương ứng với `PressedBg`, dùng cho các hiệu ứng animation.           |
| `DisabledBg`                 | `SolidColorBrush` | Màu nền khi nút hoặc thành phần bị vô hiệu hóa (`IsEnabled = false`).                 |
| `DisabledForeground`         | `SolidColorBrush` | Màu chữ khi thành phần bị vô hiệu hóa.                                                |

---

## 🛠️ Gợi ý sử dụng

- Dùng các resource này trong các `Style` hoặc `ControlTemplate` để dễ bảo trì, thay đổi theme.
- Khi muốn dùng animation (`ColorAnimation`), nên dùng các key dạng `Color`, không phải `SolidColorBrush`.

---

## 📦 Tip cho dev

Kết hợp file này với các `Style` riêng cho `Button`, `TextBlock`, `Window`, v.v. sẽ giúp bạn tạo nên một UI đồng nhất, dễ bảo trì, và chuẩn sản phẩm.

> P.S: Bạn có thể đổi màu ở đây để dễ dàng tạo các theme Dark/Light nếu cần 😉
