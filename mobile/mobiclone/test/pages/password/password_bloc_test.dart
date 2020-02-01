import 'package:flutter_test/flutter_test.dart';
import 'package:mobiclone/data/user_repository.dart';
import 'package:mobiclone/models/user.dart';
import 'package:mobiclone/pages/password/password_bloc.dart';
import 'package:mobiclone/pages/password/password_event.dart';
import 'package:mobiclone/pages/password/password_state.dart';
import 'package:mobiclone/services/user_service.dart';
import 'package:mockito/mockito.dart';

class MockUserRepository extends Mock implements UserRepository {}

class MockUserService extends Mock implements UserService {}

void main() {
  group('PasswordBloc should', () {
    MockUserRepository _userRepository;
    MockUserService _userService;
    PasswordBloc _bloc;

    setUp(() {
      _userRepository = MockUserRepository();
      _userService = MockUserService();
      _bloc = PasswordBloc(_userRepository, _userService);
    });

    tearDown(() {
      _bloc.close();
    });

    test(
      'have the initial state equal to "InitiatedPasswordState"',
      () {
        expect(_bloc.initialState, InitiatedPasswordState());
      },
    );

    test(
      'return "CreatedUserPasswordState" when the user is created',
      () {
        final User user = User("flavio", "flavio@email.com", "test123");

        when(
          _userRepository.getName(),
        ).thenAnswer(
          (_) => Future.value(user.name),
        );

        when(
          _userRepository.getEmail(),
        ).thenAnswer(
          (_) => Future.value(user.email),
        );

        when(
          _userRepository.addPassword(any),
        ).thenAnswer(
          (_) => Future.value(user.password),
        );

        when(
          _userService.addUser(any),
        ).thenAnswer(
          (_) => Future.value(user),
        );

        _bloc.add(SubmitPasswordEvent(user.password));

        expectLater(
          _bloc,
          emitsInOrder([
            InitiatedPasswordState(),
            SubmittedPasswordState(user.password),
            CreatedUserPasswordState(user),
          ]),
        );
      },
    );

    test(
      "return 'UnCreatedUserPasswordState' when the user isn't created",
      () {
        final User user = User("flavio", "flavio@email.com", "test123");

        when(
          _userRepository.getName(),
        ).thenAnswer(
          (_) => Future.value(user.name),
        );

        when(
          _userRepository.getEmail(),
        ).thenAnswer(
          (_) => Future.value(user.email),
        );

        when(
          _userRepository.addPassword(any),
        ).thenAnswer(
          (_) => Future.value(user.password),
        );

        when(
          _userService.addUser(any),
        ).thenAnswer(
          (_) => Future.value(null),
        );

        _bloc.add(SubmitPasswordEvent(user.password));

        expectLater(
          _bloc,
          emitsInOrder([
            InitiatedPasswordState(),
            SubmittedPasswordState(user.password),
            UnCreatedUserPasswordState(user),
          ]),
        );
      },
    );

    test(
      'return "RequiredPasswordState" when the password submitted is empty',
      () {
        _bloc.add(SubmitPasswordEvent(''));

        expectLater(
          _bloc,
          emitsInOrder([
            InitiatedPasswordState(),
            SubmittedPasswordState(''),
            RequiredPasswordState('', 'The password must be informed.'),
          ]),
        );
      },
    );
  });
}
