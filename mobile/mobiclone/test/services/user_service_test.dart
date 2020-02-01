import 'package:flutter_test/flutter_test.dart';
import 'package:http/http.dart' as http;
import 'package:mobiclone/services/user_service.dart';
import 'package:mockito/mockito.dart';
import 'package:mobiclone/models/user.dart';

class MockClient extends Mock implements http.Client {}

class MockResponse extends Mock implements http.Response {}

void main() {
  group('UserService.addUser should', () {
    MockClient _client;
    MockResponse _response;
    UserService _userService;

    setUp(() {
      _client = MockClient();
      _response = MockResponse();
      _userService = UserService(_client);
    });

    test(
      'return the user which was added when the request is successful.',
      () async {
        when(_response.statusCode).thenReturn(201);

        when(
          _client.post(
            any,
            headers: anyNamed('headers'),
            body: anyNamed('body'),
          ),
        ).thenAnswer(
          (_) => Future.value(_response),
        );

        final User user = User('flavio', 'flavio@email.com', 'test123');

        final created = await _userService.addUser(user);

        expect(created, equals(user));
      },
    );

    test(
      'return null when the request is unsuccessful.',
      () async {
        when(_response.statusCode).thenReturn(400);

        when(
          _client.post(
            any,
            headers: anyNamed('headers'),
            body: anyNamed('body'),
          ),
        ).thenAnswer(
          (_) => Future.value(_response),
        );

        final User user = User('flavio', 'flavio@email.com', 'test123');

        final created = await _userService.addUser(user);

        expect(created, isNull);
      },
    );
  });
}
