import 'package:flutter_test/flutter_test.dart';
import 'package:mobiclone/data/user_repository.dart';
import 'package:mobiclone/pages/name/name_bloc.dart';
import 'package:mobiclone/pages/name/name_event.dart';
import 'package:mobiclone/pages/name/name_state.dart';
import 'package:mockito/mockito.dart';

class MockUserRepository extends Mock implements UserRepository {}

void main() {
  group('NameBloc should', () {
    MockUserRepository _userRepository;
    NameBloc _bloc;

    setUp(() {
      _userRepository = MockUserRepository();
      _bloc = NameBloc(_userRepository);
    });

    tearDown(() {
      _bloc.close();
    });

    test(
      'have the initial state equal to "InitiatedNameState" with empty value',
      () {
        expect(_bloc.initialState, InitiatedNameState());
      },
    );

    test(
      'return "ValidatedNameState" when the name submitted is valid',
      () {
        when(
          _userRepository.addName('flavio'),
        ).thenAnswer(
          (_) => Future.value('flavio'),
        );

        _bloc.add(SubmitNameEvent('flavio'));

        expectLater(
          _bloc,
          emitsInOrder([
            InitiatedNameState(),
            SubmittedNameState('flavio'),
            ValidatedNameState('flavio'),
          ]),
        );
      },
    );

    test(
      'return "RequiredNameState" when the name submitted is empty',
      () {
        _bloc.add(SubmitNameEvent(''));

        expectLater(
          _bloc,
          emitsInOrder([
            InitiatedNameState(),
            SubmittedNameState(''),
            RequiredNameState('', 'The name must be informed.'),
          ]),
        );
      },
    );
  });
}
