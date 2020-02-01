import 'package:bloc/bloc.dart';
import 'package:mobiclone/data/user_repository.dart';
import 'package:mobiclone/models/user.dart';
import 'package:mobiclone/pages/password/password_event.dart';
import 'package:mobiclone/pages/password/password_state.dart';
import 'package:mobiclone/services/user_service.dart';

import 'password_state.dart';

class PasswordBloc extends Bloc<PasswordEvent, PasswordState> {
  final UserRepository _userRepository;
  final UserService _userService;

  PasswordBloc(this._userRepository, this._userService);

  @override
  PasswordState get initialState => InitiatedPasswordState();

  @override
  Stream<PasswordState> mapEventToState(PasswordEvent event) async* {
    if (event is SubmitPasswordEvent) {
      yield SubmittedPasswordState(event.value);

      if (event.value.isEmpty) {
        yield RequiredPasswordState(
          event.value,
          'The password must be informed.',
        );
      } else {
        final name = await _userRepository.getName();
        final email = await _userRepository.getEmail();
        final password = await _userRepository.addPassword(event.value);

        await _userService.addUser(User(name, email, password));

        yield ValidatedPasswordState(event.value);
      }
    }
  }
}
