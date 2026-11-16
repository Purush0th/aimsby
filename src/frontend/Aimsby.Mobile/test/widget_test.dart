import 'package:aimsby_mobile/main.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:shared_preferences/shared_preferences.dart';

void main() {
  testWidgets('first launch shows the server URL screen', (tester) async {
    SharedPreferences.setMockInitialValues({});

    await tester.pumpWidget(const AimsbyApp());
    await tester.pumpAndSettle();

    expect(find.text('Connect to your server'), findsOneWidget);
    expect(find.text('Connect'), findsOneWidget);
  });
}
