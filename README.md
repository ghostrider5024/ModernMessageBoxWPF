# 💬 Modern MessageBox for WPF (No MVVM Required)

Hộp thoại hiện đại, async/await hỗ trợ hiển thị từ bất kỳ đâu trong WPF mà không cần MVVM.  
Thiết kế gọn gàng, dễ tích hợp, có thể thay theme và tuỳ biến biểu tượng linh hoạt.

---

## 🎨 Tính năng nổi bật

- ✨ UI hiện đại, tối giản
- 🔄 Hỗ trợ `async/await`
- ⚠️ Hỗ trợ nhiều kiểu thông báo: Thông tin, Cảnh báo, Lỗi, Xác nhận
- 🧠 Không cần MVVM — gọi thẳng từ `code-behind`
- 🌈 Dễ tuỳ chỉnh theme qua `BuildThemeURL`

---

## ⚙️ Cài đặt và tích hợp

### 1. Thêm Resource vào App.xaml

```xml
<Application.Resources>
    <ResourceDictionary>
        <!-- <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/ModernMessageBoxWPF;component/Themes/Light.xaml" />
        </ResourceDictionary.MergedDictionaries> -->
        <modernMessageBox:ModernMessgeBoxConverter x:Key="DialogIconConverter" />
    </ResourceDictionary>
</Application.Resources>
```

### 2. Show MessageBox bằng

```csharp
var result = await ModernMessageBoxManager.ShowDialogAsync(
    ownerWindow,
    "Tiêu đề Dialog",
    "Nội dung message muốn hiển thị",
    MessageBoxStateType.Warning,                    // Loại message box (mặc định Info)
    ModernMessageBoxInputTypeEnum.Password,         // Kiểu input (mặc định Normal)
    myCustomStyle,                                  // Style tùy chỉnh (null nếu không dùng)
    myBuildThemeURLDelegate                         // Delegate build theme URL (null nếu không dùng)
);

```

## 🧩 Tham số của ShowDialogAsync

| Tham số         | Kiểu dữ liệu                         | Mô tả                                        | Mặc định                               |
| --------------- | ------------------------------------ | -------------------------------------------- | -------------------------------------- |
| `owner`         | `Window?`                            | Cửa sổ cha sở hữu dialog                     | `null`                                 |
| `title`         | `string`                             | Tiêu đề dialog                               | **Bắt buộc**                           |
| `message`       | `string`                             | Nội dung message                             | **Bắt buộc**                           |
| `type`          | `MessageBoxStateType`                | Loại message box (Info, Warning, Error, v.v) | `MessageBoxStateType.Info`             |
| `inputType`     | `ModernMessageBoxInputTypeEnum`      | Kiểu input (Normal, Password, None, v.v)     | `ModernMessageBoxInputTypeEnum.Normal` |
| `style`         | `ModernMessageBoxStyle?`             | Style tùy chỉnh cho dialog (có thể null)     | `null`                                 |
| `BuildThemeURL` | `Func<MessageBoxStateType, string>?` | Delegate để build URL theme dựa trên `type`  | `null`                                 |

# ModernMessageBoxStyle

### `ModernMessageBoxStyle` là class để tùy chỉnh style cho các thành phần chính của dialog `ModernMessageBox`.

## Thuộc tính

| Thuộc tính           | Kiểu dữ liệu | Mô tả                                                                               |
| -------------------- | ------------ | ----------------------------------------------------------------------------------- |
| `RootBorderStyle`    | `Style`      | Style áp dụng cho phần viền gốc của dialog (ví dụ: border color, radius, shadow...) |
| `CancelButtonStyle`  | `Style`      | Style cho nút "Cancel" trong dialog (color, font, kích thước,...)                   |
| `ConfirmButtonStyle` | `Style`      | Style cho nút "Confirm" (OK/Yes) để làm nổi bật nút xác nhận với màu sắc riêng biệt |

## Ví dụ sử dụng

```csharp
var style = new ModernMessageBoxStyle
{
    RootBorderStyle = (Style)Application.Current.FindResource("MyDialogBorderStyle"),
    CancelButtonStyle = (Style)Application.Current.FindResource("MyCancelButtonStyle"),
    ConfirmButtonStyle = (Style)Application.Current.FindResource("MyConfirmButtonStyle")
};
```

### 3. Các thuộc tính của ModernMessageBox

## 🧩 Thuộc tính (Dependency Properties)

| Tên Property           | Kiểu dữ liệu                        | Mặc định       | Mô tả                                              |
| ---------------------- | ----------------------------------- | -------------- | -------------------------------------------------- |
| `AnimationType`        | `ModernMessageBoxAnimationTypeEnum` | `SlideFromTop` | Kiểu hiệu ứng khi hiển thị và ẩn message box.      |
| `AnimationInDuration`  | `TimeSpan`                          | `00:00:00.250` | Thời gian chạy animation khi hiển thị message box. |
| `AnimationOutDuration` | `TimeSpan`                          | `00:00:00.250` | Thời gian chạy animation khi đóng message box.     |
| `CancelButtonStyle`    | `Style`                             | `00:00:00.250` | Thời gian chạy animation khi đóng message box.     |
| `AnimationOutDuration` | `TimeSpan`                          | `00:00:00.250` | Thời gian chạy animation khi đóng message box.     |
| `MessageBoxStateType`  | `ModernMessageBoxAnimationTypeEnum` | `Info`         | Trạng thái của MessageBox                          |

### 4. Các enum được sử dụng

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

## ⚙️ Giá trị enum MessageBoxStateType

| Enum Value | Mô tả                                  |
| ---------- | -------------------------------------- |
| `Info`     | Màu chủ đạo xanh nước biển _(default)_ |
| `Warning`  | Màu chủ đạo xanh cam                   |
| `Error`    | Màu chủ đạo đỏ                         |
| `Success`  | Màu chủ đạo xanh lá                    |

## ⚙️ Giá trị enum MessageBoxStateType

| Enum Value      | Mô tả                                                              |
| --------------- | ------------------------------------------------------------------ |
| `Normal`        | Chỉ hiện text khi MessageBox đóng trả về bool _(default)_          |
| `InputText`     | Hiện ra TextBox để nhập text khi MessageBox đóng trả về string     |
| `InputPassword` | Hiện ra PasswordBox để nhập text khi MessageBox đóng trả về string |

### 5. Các theme có sẵn

## 🎨 Resource Dictionary - Giao diện Modern cho MessageBox

Chứa tập hợp các brush và màu sắc được sử dụng cho MessageBox kiểu hiện đại. Bạn có thể tái sử dụng các resource này cho bất kỳ nút, hộp thoại, hay thành phần giao diện nào để đảm bảo giao diện nhất quán.

Các theme có sẵn
| MessageBoxStateType | Địa chỉ |
| ---------- | -------------------------------------- |
| `Info` | /Themes/InfoTheme.xaml |
| `Warning` | /Themes/WarningTheme.xaml |
| `Error` | /Themes/ErrorTheme.xaml |
| `Success` | /Themes/SuccessTheme.xaml |

### 📦 Tip cho dev: Có thể custom theme bằng cách truyền hàm trả về URL thông qua `MessageBoxStateType` được truyền trong `ModernMessageBoxManager.ShowDialogAsync`

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

> P.S: Vẫn đang viết ạ 😉

```

```
