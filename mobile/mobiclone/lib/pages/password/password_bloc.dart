import 'package:bloc/bloc.dart';
import 'package:mobiclone/data/user_repository.dart';
import 'package:mobiclone/models/user.dart';
import 'package:mobiclone/pages/password/password_event.dart';
import 'package:mobiclone/pages/password/password_state.dart';
import 'package:mobiclone/services/user_service.dart';

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
        final String name = await _userRepository.getName();
        final String email = await _userRepository.getEmail();
        final String password = await _userRepository.addPassword(event.value);

        final User user = User(name, email, password);

        final User created = await _userService.addUser(user);

        if (created != null) {
          yield CreatedUserPasswordState(created);
        } else {
          yield UnCreatedUserPasswordState(user);
        }
      }
    }
  }
}
