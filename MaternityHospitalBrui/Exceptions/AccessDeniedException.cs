using System;

namespace MaternityHospitalBrui.Exceptions {
  public class AccessDeniedException: Exception {
    public AccessDeniedException()
        : this("Недостаточно прав на это действие") {
    }

    public AccessDeniedException(string message)
        : base(message) {
    }
  }
}